using System;
using System.Data.SqlClient;

namespace Diplom
{
    public class WorkSessionManager : WorkTimer
    {
        private int userId;
        private string connectionString = DatabaseConfig.connectionString;
        private bool isRunning;
        private bool lunchBonusAdded = false;
 

        public WorkSessionManager(int userId, System.Windows.Forms.Label timerLabel) : base(timerLabel)
        {
            this.userId = userId;
            isRunning = false; 
        }

        public void SaveWorkedTime(int currentElapsedSeconds)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string checkQuery = @"
                SELECT TOP 1 SessionID, EffectiveTime 
                FROM WorkSessions 
                WHERE UserID = @UserID 
                AND CAST(StartTime AS DATE) = CAST(GETDATE() AS DATE) 
                ORDER BY StartTime DESC";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@UserID", userId);
                        using (SqlDataReader reader = checkCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int sessionId = reader.GetInt32(0);
                                int effectiveTimeInDb = reader.GetInt32(1);

                                // 👉 Вычисляем разницу
                                int secondsToSave = currentElapsedSeconds - effectiveTimeInDb;

                                // ⛔ Ничего не сохраняем, если нет новой работы
                                if (secondsToSave <= 0)
                                    return;

                                int maxWorkTime = 8 * 3600;
                                int totalTime = effectiveTimeInDb + secondsToSave;

                                reader.Close();

                                if (totalTime <= maxWorkTime)
                                {
                                    UpdateWorkSession(sessionId, secondsToSave, conn);
                                }
                                else
                                {
                                    int overtimeSeconds = totalTime - maxWorkTime;
                                    int regularTime = secondsToSave - overtimeSeconds;

                                    if (regularTime > 0)
                                    {
                                        UpdateWorkSession(sessionId, regularTime, conn);
                                    }

                                    if (overtimeSeconds > 0)
                                    {
                                        SaveOvertime(overtimeSeconds, conn);
                                    }
                                }
                            }
                            else
                            {
                                reader.Close();
                                CreateNewWorkSession(currentElapsedSeconds, conn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка сохранения времени работы: " + ex.Message);
                }
            }
        }

        private void UpdateWorkSession(int sessionId, int workedSeconds, SqlConnection conn)
        {
            string updateQuery = @"
                UPDATE WorkSessions 
                SET 
                    EffectiveTime = EffectiveTime + @WorkedSeconds,
                    EndTime = GETDATE()
                WHERE SessionID = @SessionID";

            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
            {
                updateCmd.Parameters.AddWithValue("@WorkedSeconds", workedSeconds);
                updateCmd.Parameters.AddWithValue("@SessionID", sessionId);
                updateCmd.ExecuteNonQuery();
            }
        }


        private void CreateNewWorkSession(int workedSeconds, SqlConnection conn)
        {
            string insertQuery = @"
                INSERT INTO WorkSessions (UserID, StartTime, EffectiveTime) 
                VALUES (@UserID, GETDATE(), @WorkedSeconds)";

            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
            {
                insertCmd.Parameters.AddWithValue("@UserID", userId);
                insertCmd.Parameters.AddWithValue("@WorkedSeconds", workedSeconds);
                insertCmd.ExecuteNonQuery();
            }
        }

        private void SaveOvertime(int overtimeSeconds, SqlConnection conn)
        {
            string insertOvertimeQuery = @"
                INSERT INTO OvertimeSessions (UserID, StartTime, ExtraTime) 
                VALUES (@UserID, GETDATE(), @OvertimeSeconds)";

            using (SqlCommand overtimeCmd = new SqlCommand(insertOvertimeQuery, conn))
            {
                overtimeCmd.Parameters.AddWithValue("@UserID", userId);
                overtimeCmd.Parameters.AddWithValue("@OvertimeSeconds", overtimeSeconds);
                overtimeCmd.ExecuteNonQuery();
            }
        }
        public void AddLunchBonus(TimeSpan duration)
        {
            if (lunchBonusAdded) return;

            secondsElapsed += (int)duration.TotalSeconds;
            lunchBonusAdded = true;
        }

        public override void Start()
        {
            if (!isRunning)
            {
                base.Start();
                isRunning = true;
            }
        }

        public override void Stop()
        {
            if (isRunning)
            {
                base.Stop();
                SaveWorkedTime(secondsElapsed);
                isRunning = false;
            }
        }
    }
}
