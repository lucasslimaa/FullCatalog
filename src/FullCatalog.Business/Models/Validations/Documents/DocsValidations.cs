using System.Collections.Generic;
using System.Linq;

namespace FullCatalog.Business.Models.Validations.Documents
{
    public class CpfValidacao
    {
        public const int CpfLength = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumbers = Utils.OnlyNumbers(cpf);

            if (!ValidatedLength(cpfNumbers)) return false;
            return !HasRepetedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
        }

        private static bool ValidatedLength(string value)
        {
            return value.Length == CpfLength;
        }

        private static bool HasRepetedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, CpfLength - 2);
            var verificationDigit = new verificationDigit(number)
                .WithMultiplesRange(2, 11)
                .Replacing("0", 10, 11);
            var firstDigit = verificationDigit.CalculateDigit();
            verificationDigit.AddDigito(firstDigit);
            var secondDigit = verificationDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(CpfLength - 2, 2);
        }
    }

    public class CnpjValidation
    {
        public const int CnpjLength = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumbers = Utils.OnlyNumbers(cpnj);

            if (!HasValidLength(cnpjNumbers)) return false;
            return !HasRepetedDigits(cnpjNumbers) && HasValidDigits(cnpjNumbers);
        }

        private static bool HasValidLength(string value)
        {
            return value.Length == CnpjLength;
        }

        private static bool HasRepetedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, CnpjLength - 2);

            var verificationDigit = new verificationDigit(number)
                .WithMultiplesRange(2, 9)
                .Replacing("0", 10, 11);
            var firstDigit = verificationDigit.CalculateDigit();
            verificationDigit.AddDigito(firstDigit);
            var secondDigit = verificationDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(CnpjLength - 2, 2);
        }
    }

    public class verificationDigit
    {
        private string _number;
        private const int Modulo = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _replacements = new Dictionary<int, string>();
        private bool _ModuloComplement = true;

        public verificationDigit(string number)
        {
            _number = number;
        }

        public verificationDigit WithMultiplesRange(int primeiroMultiplicador, int ultimoMultiplicador)
        {
            _multipliers.Clear();
            for (var i = primeiroMultiplicador; i <= ultimoMultiplicador; i++)
                _multipliers.Add(i);

            return this;
        }

        public verificationDigit Replacing(string substituto, params int[] digitos)
        {
            foreach (var i in digitos)
            {
                _replacements[i] = substituto;
            }
            return this;
        }

        public void AddDigito(string digito)
        {
            _number = string.Concat(_number, digito);
        }

        public string CalculateDigit()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var soma = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
                soma += produto;

                if (++m >= _multipliers.Count) m = 0;
            }

            var mod = (soma % Modulo);
            var resultado = _ModuloComplement ? Modulo - mod : mod;

            return _replacements.ContainsKey(resultado) ? _replacements[resultado] : resultado.ToString();
        }
    }

    public class Utils
    {
        public static string OnlyNumbers(string value)
        {
            var onlyNumber = "";
            foreach (var s in value)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }
}
