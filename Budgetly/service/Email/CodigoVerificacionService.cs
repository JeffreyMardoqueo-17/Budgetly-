namespace Budgetly.Services.Auth
{
    public class CodigoVerificacionService
    {
        private string? _codigo;
        private DateTime _expira;

        public string GenerarCodigo()
        {
            var random = new Random();
            _codigo = random.Next(100000, 999999).ToString(); // Código de 6 dígitos
            _expira = DateTime.UtcNow.AddMinutes(3);
            return _codigo;
        }

        public bool VerificarCodigo(string codigoIngresado)
        {
            if (_codigo == null || DateTime.UtcNow > _expira)
                return false;

            return _codigo == codigoIngresado;
        }

        public bool CodigoExpirado() => DateTime.UtcNow > _expira;
    }
}
