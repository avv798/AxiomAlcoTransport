using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Функционал файлового хранилища приложения.
    /// </summary>
    public class FileStorage
    {
        #region Внешние константы класса.
        /// <summary>
        /// Расширение имени "родных" файлов.
        /// Константа.
        /// </summary>
        public const string NativePrefix = "informa";
        /// <summary>
        /// Расширение имени "родных" файлов.
        /// Константа.
        /// </summary>
        public const string NativeExtension = "axioma.xml";
        /// <summary>
        /// Архиватор.
        /// Константа.
        /// </summary>
        public const string Archiver = "Utilities\\rar.exe";
        #endregion Защищённые константы класса.

        #region Защищенные объекты класса.
        /// <summary>
        /// Включение или отключение логирования процесса работы (для массовых операций).
        /// Только чтение.
        /// </summary>
        protected readonly bool verbose;
        /// <summary>
        /// Путь к вершине каталога локальной базы данных.
        /// Только чтение.
        /// </summary>
        protected readonly string path;
        /// <summary>
        /// Имя файла для сохранения списка событий.
        /// Только чтение.
        /// </summary>
        protected string eventsFilename
        {
            get { return string.Format("{0}\\{1}.online.events.{2}", PathEvents, NativePrefix, NativeExtension); }
        }
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Путь к вершине каталога локальной базы данных.
        /// Только чтение.
        /// </summary>
        public string Path
        {
            get { return path; }
        }
        /// <summary>
        /// Путь к каталогу архивных копий.
        /// Только чтение.
        /// </summary>
        public string PathArchives
        {
            get { return string.Format("{0}\\Archives", path); }
        }
        /// <summary>
        /// Путь к хранилищу данных.
        /// Только чтение.
        /// </summary>
        public string PathData
        {
            get { return string.Format("{0}\\Data", path); }
        }
        /// <summary>
        /// Путь к списку событий.
        /// Только чтение.
        /// </summary>
        public string PathEvents
        {
            get { return string.Format("{0}\\Events", path); }
        }
        /// <summary>
        /// Путь к "корзине".
        /// Только чтение.
        /// </summary>
        public string PathGarbage
        {
            get { return string.Format("{0}\\Garbage", path); }
        }
        /// <summary>
        /// Путь к отправленным запросам.
        /// Только чтение.
        /// </summary>
        public string PathOut
        {
            get { return string.Format("{0}\\Out", path); }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected FileStorage()
        {
            verbose = false;
            path = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="path">Путь к вершине каталога локальной базы данных.</param>
        /// <param name="verbose">Включить логирование процесса работы с файлами (для массовых операций).</param>
        public FileStorage(string path, bool verbose = false) : this()
        {
            this.path = path;
            this.verbose = verbose;

            if (string.IsNullOrWhiteSpace(Path)) throw new Exception("Не задан путь к вершине каталога локальной базы данных.");

            Program.Logger.Info(this, "Объект инициализирован успешно.");
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Подготовить базу данных к старту.
        /// </summary>
        public void Prepare()
        {
            try
            {
                Program.Logger.Info(this, "Попытка подготовить базу данных к старту...");

                prepare();

                Program.Logger.Info(this, "... база данных успешно подготовлена к старту.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время подготовки базы данных произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Сделать резервную копию базы данных.
        /// </summary>
        public void Backup()
        {
            try
            {
                Program.Logger.Info(this, "Попытка создать резервную копию базы данных...");

                if (!Program.GetBooleanParameter("createBackup"))
                {
                    Program.Logger.Warn(this, "Создание резервной копии запрещено конфигурационным файлом приложения.");

                    return;
                }

                string str = backup();

                Program.Logger.Info(this, string.Format("... резервная копия базы данных успешно создана: '{0}'.", str));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время создания резервной копии базы данных произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Сохранить документ в хранилище.
        /// </summary>
        /// <param name="document">Документ.</param>
        public void Save(ADocument document)
        {
            try
            {
                if (verbose) Program.Logger.Info(this, string.Format("Попытка сохранить документ '{0}' в хранилище...", document.Description));

                save(document);

                if (verbose) Program.Logger.Info(this, string.Format("... документ успешно сохранён в файле '{0}'.", document.FileName));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения документа произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="enableLogging">Включение или отключение логгирования.</param>
        public void Delete(ADocument document, bool enableLogging = true)
        {
            try
            {
                if (enableLogging) Program.Logger.Info(this, string.Format("Попытка удалить документ '{0}' из хранилища...", document.Description));

                delete(document);

                if (enableLogging) Program.Logger.Info(this, string.Format("... документ успешно удалён (файл '{0}').", document.FileName));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время удаления документа произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Загрузить объект.
        /// </summary>
        /// <typeparam name="T">Тип объекта для загрузки.</typeparam>
        /// <param name="filename">Полное имя файла.</param>
        /// <returns>Объект.</returns>
        public T Load<T>(string filename)
            where T : ADocument
        {
            try
            {
                if (verbose) Program.Logger.Info(this, string.Format("Попытка загрузить файл '{0}' из хранилища...", filename));

                T obj = load<T>(filename);

                if (verbose) Program.Logger.Info(this, string.Format("... документ '{0}' успешно загружен.", obj.Description));

                return obj;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки документа произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Получить список файлов хранилища.
        /// </summary>
        /// <returns>Список.</returns>
        public IEnumerable<string> GetFileList()
        {
            try
            {
                string mask = string.Format("*.{0}", NativeExtension);

                Program.Logger.Info(this, string.Format("Попытка получить список файлов хранилища по маске '{0}'...", mask));

                IEnumerable<string> list = Directory.GetFiles(PathData, mask);

                Program.Logger.Info(this, string.Format("... список файлов хранилища (в количестве {0} штук) успешно загружен.", list.Count()));

                return list;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки списка файлов базы данных произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Получить список файлов корзины.
        /// </summary>
        /// <returns>Список файлов корзины.</returns>
        public IEnumerable<string> GetGarbageFileList()
        {
            try
            {
                string mask = string.Format("*.{0}", NativeExtension);

                Program.Logger.Info(this, string.Format("Попытка получить список файлов каталога корзины по маске '{0}'...", mask));

                IEnumerable<string> list = Directory.GetFiles(PathGarbage, mask);

                Program.Logger.Info(this, string.Format("... список файлов каталога корзины (в количестве {0} шт.) успешно загружен.", list.Count()));

                return list;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки списка файлов каталога корзины произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Перенести документ в корзину.
        /// </summary>
        /// <param name="document">Документ.</param>
        public void MoveToGarbage(ADocument document)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка перенести документ '{0}' в корзину...", document.Description));

                moveToGarbage(document);

                Program.Logger.Info(this, string.Format("... файл '{0}' успешно перенесён в каталог корзины.", document.FileName));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения документа произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Восстановить документ из корзины.
        /// </summary>
        /// <param name="document">Документ.</param>
        public void RestoreFromGarbage(ADocument document)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка восстановить документ '{0}' из корзины...", document.Description));

                restoreFromGarbage(document);

                Program.Logger.Info(this, string.Format("... файл '{0}' успешно восстановлен из каталога корзины и перенесён в каталог данных.", document.FileName));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения документа произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Загрузить список событий.
        /// </summary>
        /// <returns>Список.</returns>
        public List<OnlineEvent> LoadOnlineEvents()
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузить список событий...");

                List<OnlineEvent> list = loadOnlineEvents();

                Program.Logger.Info(this, string.Format("... список ранее сохранённых событий (в количестве {0} шт.) успешно загружен.", list.Count));

                return list;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки списка событий произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Сохранить список событий.
        /// </summary>
        /// <param name="list">Список.</param>
        public void SaveOnlineEvents(List<OnlineEvent> list)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка сохранить список событий (в количестве {0} шт.)...", list.Count));

                saveOnlineEvents(list);

                Program.Logger.Info(this, "... список событий успешно сохранён.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения списка событий произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Осуществить поиск в файлах базы данных.
        /// </summary>
        /// <param name="findtext">Строка для поиска.</param>
        /// <returns>Файлы, содержащие указанную строку.</returns>
        public List<string> FullTextSearch(string findtext)
        {
            Program.Logger.Info(this, string.Format("Попытка выполнения полнотекстового поиска по строке '{0}'... ", findtext));

            List<string> list = fullTextSearch(findtext);

            Program.Logger.Info(this, string.Format("... выполнение полнотекстового поиска по строке '{0}' завершено; найдено файлов: {1}.", findtext, list.Count));

            return list;
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Подготовить базу данных к старту.
        /// </summary>
        protected virtual void prepare()
        {
            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            if (!Directory.Exists(PathArchives)) Directory.CreateDirectory(PathArchives);
            if (!Directory.Exists(PathData)) Directory.CreateDirectory(PathData);
            if (!Directory.Exists(PathEvents)) Directory.CreateDirectory(PathEvents);
            if (!Directory.Exists(PathGarbage)) Directory.CreateDirectory(PathGarbage);
            if (!Directory.Exists(PathOut)) Directory.CreateDirectory(PathOut);

            string readme = string.Format("{0}\\readme.txt", Path);

            string note = string.Format("=============================\r\n" +
                                        "Infroma.AlcoTransport.Storage\r\n" +
                                        "=============================\r\n" +
                                        "File created at: {0}.\r\n" +
                                        "=============================\r\n" +
                                        "Main catalogue: '{1}'.\r\n" +
                                        "Other cataloques:\r\n" +
                                        " - '{2}' - backup catalogue (only data's files);\r\n" +
                                        " - '{3}' - data's catalogue;\r\n" +
                                        " - '{4}' - events catalogue;\r\n" +
                                        " - '{5}' - garbage catalogue;\r\n" +
                                        " - '{6}' - catalogue with copies of out-requests.\r\n" +
                                        "=============================\r\n",
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                        Path,
                                        PathArchives,
                                        PathData,
                                        PathEvents,
                                        PathGarbage,
                                        PathOut);

            if (File.Exists(readme))
            {
                FileInfo info = new FileInfo(readme);
                if (info.Length == note.Length) return;
            }

            File.WriteAllText(readme, note);
        }
        /// <summary>
        /// Сделать резервную копию базы данных.
        /// </summary>
        /// <returns>Путь к резервной копии.</returns>
        protected virtual string backup()
        {
            if (!File.Exists(Archiver)) throw new FileNotFoundException("Отсутствует архиватор 'WinRAR'. Создание резервной копии невозможно.", Archiver);

            // Копия будет создаваться один раз в день в момент первого запуска приложения; создавать копию каждый каждый раз во время запуска - накладно.
            // string copy = string.Format("{0}\\InformaAlcoTransportStorageBackup-{1}.rar", PathArchives, DateTime.Now.ToString("yyyyMMddHHmmss"));

            string copy = string.Format("{0}\\InformaAlcoTransportStorageBackup-{1}.rar", PathArchives, DateTime.Now.ToString("yyyyMMdd"));

            if (File.Exists(copy))
            {
                Program.Logger.Info(this, string.Format("... резервная копия '{0}' уже создана; новое создание не требуется...", copy));

                return copy;
            }

            // Исходящие документы не архивируются.
            // string paths = string.Format("\"{0}\\*.{4}\" \"{1}\\{5}.*.{4}\" \"{2}\\*.{4}\" \"{3}\\{5}.*.{4}\"",
            //                              PathData, PathEvents, PathGarbage, PathOut, NativeExtension, NativePrefix);

            // Документы корзины не архивируются.
            // string paths = string.Format("\"{0}\\*.{4}\" \"{1}\\{3}.*.{4}\" \"{2}\\*.{4}\"",
            //                              PathData, PathEvents, PathGarbage, NativePrefix, NativeExtension);

            // Для ускорения процесса резервирования, архивируется только каталог с данными (без справочника алкогольной продукции)
            // и каталог с системными событиями.
            string paths = string.Format("\"{0}\\*.{3}\" \"{1}\\{2}.*.{3}\"",
                                         PathData, PathEvents, NativePrefix, NativeExtension);

            string exclude = string.Format("\"{0}\\*\\Axiom.AlcoTransport.Production.*.axioma.xml\"", Path);
            
            // Архивируются все файлы за исключением справочников алкогольной продукции.
            // string arguments = string.Format(" a -r {0} {1}", copy, paths);
            string arguments = string.Format(" a -r -x{0} \"{1}\" {2}", exclude, copy, paths);

            Program.Logger.Info(this, string.Format("... команда для создания резервной копии: '{0}{1}'...", Archiver, arguments));

            using (Process process = new Process
                                            {
                                                StartInfo =
                                                {
                                                    CreateNoWindow = true,
                                                    WindowStyle = ProcessWindowStyle.Hidden,
                                                    FileName = Archiver,
                                                    Arguments = arguments,
                                                }
                                            })
            {
                process.Start();

                // Асинхронный режим выполнения включить нельзя.
                // WinRAR блокирует файлы во время архивации, из-за чего может возникать ошибка доступа во время загрузки хранилища.
                process.WaitForExit();
            }

            return copy;
        }
        /// <summary>
        /// Сохранить документ в хранилище.
        /// <remarks>Важно! Метод нуждается в изменении.</remarks>
        /// </summary>
        /// <param name="document">Документ.</param>
        protected virtual void save(ADocument document)
        {
            using (Stream stream = File.Create(string.Format("{0}\\{1}", PathData, document.FileName)))
            {
                DataContractSerializer serializer = new DataContractSerializer(document.GetType());
                
                serializer.WriteObject(stream, document);

                stream.Close();
            }
        }
        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="document">Документ.</param>
        protected virtual void delete(ADocument document)
        {
            string filename = string.Format("{0}\\{1}", PathData, document.FileName);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            else
            {
                Program.Logger.Warn(this, string.Format("Файл '{0}' отсутствует в хранилище.", document.FileName));
            }
        }
        /// <summary>
        /// Загрузить объект.
        /// <remarks>Важно! Метод нуждается в изменении.</remarks>
        /// </summary>
        /// <typeparam name="T">Тип объекта для загрузки.</typeparam>
        /// <param name="filename">Полное имя файла.</param>
        /// <returns>Объект.</returns>
        protected virtual T load<T>(string filename)
            where T : ADocument
        {
            T obj;

            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));

                obj = (T)serializer.ReadObject(stream);

                stream.Close();
            }

            return obj;
        }
        /// <summary>
        /// Перенести документ в корзину.
        /// </summary>
        /// <param name="document">Документ.</param>
        protected virtual void moveToGarbage(ADocument document)
        {
            string source = string.Format("{0}\\{1}", PathData, document.FileName);
            string target = string.Format("{0}\\{1}", PathGarbage, document.FileName);

            if (File.Exists(source))
            {
                File.Move(source, target);
            }
            else
            {
                string message = string.Format("Файл '{0}' отсутствует в хранилище.", document.FileName);

                Program.Logger.Error(this, message);

                throw new Exception(message);
            }
        }
        /// <summary>
        /// Восстановить документ из корзины.
        /// </summary>
        /// <param name="document">Документ.</param>
        protected virtual void restoreFromGarbage(ADocument document)
        {
            string source = string.Format("{0}\\{1}", PathGarbage, document.FileName);
            string target = string.Format("{0}\\{1}", PathData, document.FileName);

            if (File.Exists(source))
            {
                File.Move(source, target);
            }
            else
            {
                string message = string.Format("Файл '{0}' отсутствует в каталоге корзины.", document.FileName);

                Program.Logger.Error(this, message);

                throw new Exception(message);
            }
        }
        /// <summary>
        /// Загрузить список событий.
        /// </summary>
        /// <returns>Список.</returns>
        protected virtual List<OnlineEvent> loadOnlineEvents()
        {
            List<OnlineEvent> list = new List<OnlineEvent>();

            if (File.Exists(eventsFilename))
            {
                using (Stream stream = File.Open(eventsFilename, FileMode.Open))
                {
                    DataContractSerializer serializer = new DataContractSerializer(list.GetType());

                    list = (List<OnlineEvent>) serializer.ReadObject(stream);

                    stream.Close();
                }
            }
            else
            {
                Program.Logger.Warn(this, string.Format("Файл '{0}' не обнаружен. Загрузка событий не производится.", eventsFilename));
            }

            return list;
        }
        /// <summary>
        /// Сохранить список событий.
        /// </summary>
        /// <param name="list">Список.</param>
        protected virtual void saveOnlineEvents(List<OnlineEvent> list)
        {
            using (Stream stream = File.Create(eventsFilename))
            {
                DataContractSerializer serializer = new DataContractSerializer(list.GetType());

                serializer.WriteObject(stream, list);

                stream.Close();
            }
        }
        /// <summary>
        /// Осуществить поиск в файлах базы данных.
        /// </summary>
        /// <param name="findtext">Строка для поиска.</param>
        /// <returns>Файлы, содержащие указанную строку.</returns>
        protected virtual List<string> fullTextSearch(string findtext)
        {
            List<string> list = new List<string>();

            foreach (string filename in GetFileList())
            {
                if (string.IsNullOrWhiteSpace(filename)) continue;

                string onlyname = System.IO.Path.GetFileName(filename);

                if (string.IsNullOrWhiteSpace(onlyname)) continue;

                if ((onlyname.StartsWith(string.Format("{0}.", typeof(Production).FullName)))
                    || (onlyname.StartsWith(string.Format("{0}.", typeof(Partner).FullName)))
                    || (onlyname.StartsWith(string.Format("{0}.", typeof(FormBRegInfo).FullName)))
                    || (onlyname.StartsWith(string.Format("{0}.", typeof(InventoryBRegInfo).FullName)))) continue;

                string body = File.ReadAllText(filename);

                if (body.IndexOf(findtext, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    list.Add(filename);

                    // if (list.Count > 256) throw new Exception("Выполнение полнотекстового поиска прервано.\r\n" +
                    //                                             "Указанному условию соответствует слишком большое число документов в базе данных.\r\n" +
                    //                                             "Попробуйте уточнить строку для поиска.");
                }
            }

            return list;
        }
        #endregion Защищенные методы класса.
    }
}
