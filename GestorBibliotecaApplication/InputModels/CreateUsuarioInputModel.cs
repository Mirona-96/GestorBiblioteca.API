namespace GestorBibliotecaApplication.InputModels
{
    public class CreateUsuarioInputModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}