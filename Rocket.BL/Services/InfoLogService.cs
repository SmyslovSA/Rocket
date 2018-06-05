using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Rocket.BL.Services
{
    public class InfoLogService
    {
        // количество байт для буфера
        private const int СountBytes = 4096;

        /// <summary>
        /// Получить последних N строк из логгера
        /// </summary>
        /// <returns> string </returns>
        public string GetLogInfo() // todo MP подумать чтобы передать дату файла вместо =string path=, =int count=
        {
            var fileName = $"{DateTime.Now:shortdate}.log";
            var path = "..\\App_data\\Logs";
            const int count = 20; // количество последних записей

            path = Path.Combine(path, fileName);

            var resultString = string.Empty;

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path) || count <= 0)
            {
                return resultString;
            }

            var buffer = new byte[СountBytes];
            var logRows = new StringBuilder();

            // прочитать N байт с файла
            using (var fStream = new FileInfo(path).Open(FileMode.Open, FileAccess.Read))
            {
                if (fStream.Length <= СountBytes)
                {
                    var readed = fStream.Read(buffer, 0, buffer.Length);
                    logRows.Insert(0, Encoding.UTF8.GetString(buffer, 0, readed));
                }
                else
                {
                    // рассчитываем точку входа в последний буфер с учетом кратности 
                    var multiply = fStream.Length % СountBytes;
                    var position = multiply * СountBytes;

                    // количество полных комплит-строк
                    var countFullLines = 0;

                    do
                    {
                        fStream.Position = position;
                        var readed = fStream.Read(buffer, 0, СountBytes);
                        var readedStr = Encoding.UTF8.GetString(buffer, 0, readed);

                        // подсчёт строк
                        countFullLines += readedStr.Count(t => t == '\n') - 1;

                        logRows.Insert(0, readedStr);

                        if (position == 0)
                        {
                            break;
                        }

                        position -= СountBytes;
                        if (position < 0)
                        {
                            position = 0;
                        }
                    }
                    while (countFullLines < count + 1);
                }
            }

            return GetResult(logRows, count);
        }

        /// <summary>
        /// Получить результат из строки
        /// </summary>
        /// <param name="logRows"> Строка для анализа </param>
        /// <param name="count"> Нужное количество записей </param>
        /// <returns> Результат в виде строки </returns>
        private string GetResult(StringBuilder logRows, int count)
        {
            var firstLineStart = logRows.Length;
            var lineCount = 0;
            for (; firstLineStart > 0; firstLineStart--)
            {
                if (logRows[firstLineStart] == '\n')
                {
                    lineCount++;
                }

                if (count == lineCount)
                {
                    break;
                }
            }

            return logRows.ToString(firstLineStart, logRows.Length - firstLineStart - 1);
        }
    }
}
