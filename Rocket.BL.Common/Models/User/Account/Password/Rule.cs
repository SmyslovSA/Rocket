namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Правила составления шаблона пароля
    /// </summary>
    public class Rule
    {
        // Конструктор класса.
        public Rule()
        {
            this.LenghtMin = 5; // Минимальное количество символов пароля - 5. Максимальное по умолчанию не установливается.
            
            this.DigitalSymbolsRequired = false; // По умолчанию ввод цифр в обязательном порядке не требуется.

            this.LetterSymbolsRequired = false; // По умолчанию ввод букв в обязательном порядке не требуется.

            this.SpecialSymbolsRequired = false; // По умолчанию ввод специальных символов в обязательном порядке не требуется.

            this.UpperAndLowerCaseLetterRequired = false; // По умолчанию составлять пароль, как из символов верхнего, так и нижнего регистра не требуется.
        }
        
        // Минимальная длина пароля.
        public int? LenghtMin { get; set; }

        // Максимальная длина пароля.
        public int? LenghtMax { get; set; }

        // Если буквы обязательны, то они должны быть,
        // как в верхнем, так и в нижнем регистре.
        public bool? UpperAndLowerCaseLetterRequired { get; set; }

        // Требуются цифры.
        public bool? DigitalSymbolsRequired { get; set; }

        // Минимальное количество цифр, которые требуются 
        // для ввода, если цифры в принципе требуются, то есть свойство
        // DigitallSymbolsRequired установлено в true.
        public int? MinimumDigitalSymbolsRequired { get; set; }

        // Требуются буквы.
        public bool? LetterSymbolsRequired { get; set; }

        // Минимальное количество букв, которые требуются 
        // для ввода, если буквы в принципе требуются, то есть свойство
        // DigitallSymbolsRequired установлено в true.
        public int? MinimumLetterSymbolsRequired { get; set; }

        // Требуются специальные символы при вводе пароля - @, # и т.д.
        public bool? SpecialSymbolsRequired { get; set; }

        // Минимальное количество специальных символов, которые требуются 
        // для ввода, если специальные символы в принципе требуются, то есть свойство
        // DigitallSymbolsRequired установлено в true.
        public int? MinimumSpecialSymbolsRequired { get; set; }
    }
}
