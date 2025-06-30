namespace GestorBibliotecaApplication.InputModels
{
    public class LoginInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        public LoginViewModel(string token) 
        {
            Token = token;
        }
        public string Token {  get; set; }
    }
}