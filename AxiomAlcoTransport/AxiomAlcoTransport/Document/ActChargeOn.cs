using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Акт постановки товара на баланс.
    /// </summary>
    [DisplayName("Акты постановки товара на баланс"), Serializable]
    public class ActChargeOn : Movement
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Уведомление о постановке на баланс продукции.
        /// Оригинальное тело документа.
        /// </summary>
        protected string xmlInventoryBRegInfo;
        #endregion Защищённые объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ActChargeOn()
        {
            xmlInventoryBRegInfo = string.Empty;
        }
        /// <summary>
        /// Создать новый акт на основе существующего.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        public ActChargeOn(ActChargeOn exist) : this()
        {
            fillData(exist);
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Дополнить акт данными.
        /// </summary>
        /// <param name="form">Уведомление о постановке на баланс продукции.</param>
        public void AddData(InventoryBRegInfo form)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка дополнить акт '{0}' данными уведомления о постановке на баланс продукции...", Description));

                addData(form);

                Program.Logger.Info(this, "... попытка дополнить накладную данными уведомления о постановке на баланс продукции успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время дополнения накладной данными уведомления о постановке на баланс продукции произошла ошибка.", exception);

                throw;
            }
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Дополнить накладную данными.
        /// </summary>
        /// <param name="form">Документ о регистрации движения</param>
        protected virtual void addData(InventoryBRegInfo form)
        {
            xmlInventoryBRegInfo = form.XmlBody;

            lastChange = DateTime.Now;

            treeData = buildTreeData();
        }
        #endregion Защищенные методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Построить иерархию данных.
        /// </summary>
        /// <returns>Иерархические данные.</returns>
        protected override TreeData buildTreeData()
        {
            Program.Logger.Info(this, string.Format("Построение (расширенное) иерархических данных в акте '{0}'...", Description));

            TreeData tree = base.buildTreeData();

            #region Уведомление о постановке на баланс продукции.
            {
                if (string.IsNullOrWhiteSpace(xmlInventoryBRegInfo))
                {
                    Program.Logger.Info(this, "Уведомление о постановке на баланс продукции пустое. Дополнения не требуется.");
                }
                else
                {
                    Program.Logger.Info(this, "Попытка дополнения иерархических данных уведомлением о постановке на баланс продукции....");

                    XmlDocument xmlForm = new XmlDocument();
                    xmlForm.LoadXml(xmlInventoryBRegInfo);

                    TreeData xmlConfirm = new TreeData(tree, "Акт подтверждения постановки товара на баланс на складе организации", null);

                    if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, xmlConfirm);

                    Program.Logger.Info(this, "... дополнение иерархических данных уведомлением о постановке на баланс продукции успешно завершено.");
                }
            }
            #endregion Уведомление о постановке на баланс продукции.

            Program.Logger.Info(this, "... построение (расширенное) иерархических данных успешно завершено.");

            return tree;
        }
        #endregion Переопределение базовых методов.
    }
}
