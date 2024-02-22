using System.Globalization;

namespace ExceptionHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            string surname = string.Empty;
            string name = string.Empty;
            string patronymic = string.Empty;
            DateTime birthdate;
            ulong phoneNumber;
            char gender;
            bool flag = true;

            while (flag)
            {

                try
                {
                    // Запрашиваем данные у пользователя
                    Console.WriteLine("Введите данные (фамилия имя отчество дата рождения номер телефона пол):");
                    string input = Console.ReadLine();
                    string[] data = input.Split(' ');

                    // Проверяем количество данных
                    if (data.Length != 6)
                    {
                        throw new Exception($"Неверное количество данных. Ожидалось: 6 Было получено: {data.Length} ");
                    }

                    surname = data[0];
                    name = data[1];
                    patronymic = data[2];

                    // Проверка формата даты рождения
                    if (!DateTime.TryParseExact(data[3], "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate))
                    {
                        throw new Exception("Неверный формат даты рождения");
                    }

                    // Проверка формата номера телефона
                    if (!ulong.TryParse(data[4], out phoneNumber))
                    {
                        throw new Exception("Неверный формат номера телефона");
                    }

                    // Проверка формата пола
                    if (data[5] != "f" && data[5] != "m")
                    {
                        throw new Exception("Неверный формат пола");
                    }
                    else
                    {
                        char.TryParse(data[5].ToLower(), out gender);
                    }

                    // Создаем строку для записи в файл
                    string record = $"{surname} {name} {patronymic} {birthdate} {phoneNumber} {gender}";

                    // Создаем файл с названием фамилии
                    string filename = $"{surname}.txt";
                    if (!File.Exists(filename))
                    {
                        // Если файл уже существует, добавляем запись на новой строке
                        File.WriteAllText(filename, record);
                    }
                    else
                    {
                        File.AppendAllText(filename, Environment.NewLine + record);
                        // Файл будет закрыт автоматически
                    }

                    Console.WriteLine("Данные успешно сохранены в файл.");
                    flag = false;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

            }
        }
    }
}
