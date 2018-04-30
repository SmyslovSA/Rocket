﻿namespace Rocket.Parser.Interfaces
{
    /// <summary>
    /// Базовый интерфейс парсера
    /// </summary>
    internal interface IParser
    {
        /// <summary>
        /// Запуск процесса парсинга
        /// </summary>
        /// <returns></returns>
        void Parse();
    }
}
