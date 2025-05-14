
using System.Collections.Generic;
using System.Data;
using Diplom.Security;
using Diplom.UI;

public interface IUserService
{
    DataTable GetAllUsers();
    LoginResult GetUserByLogin(string Login);
    
    void SetUserBanStatus(int userId, bool isBanned);
    List<BanReasonItem> GetBanReasons();
    void BanUser(int userId, int reasonId, string adminNote);
    void DeleteUserByLogin(string login);
    DataTable SearchUsers(string keyword);
    void SaveUserToDatabase(string login, string fullName, string Email, string _passwordHash, string _salt, bool _isEditMode, bool isBaned);
    bool ValidateFields(string login, string fullName, string Email, string _passwordHash, string _salt, bool _isEditMode);
}
