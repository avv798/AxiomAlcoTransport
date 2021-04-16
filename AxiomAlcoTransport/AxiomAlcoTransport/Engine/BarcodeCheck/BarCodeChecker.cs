using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace Axiom.AlcoTransport.Engine.BarcodeCheck
{
    /// <summary>
    /// Класс выявления расхождений по введенным штрихкодам
    /// </summary>
    public class BarCodeChecker
    {
        #region Внутренние  костанты свойства и методы
        
        private const string IdleState = nameof(IdleState);
        private const string CheckRunningState = nameof(CheckRunningState);
        private string State { get; set; }

        private void DistributeBarcode(string barcode)
        {
            var index = BarCodePositions.FindIndex(position => position.Barcode == barcode);
            if (index == -1)
                BarCodePositions.Add(new BarcodePosition {BarcodeInput = barcode});
            else BarCodePositions[index].BarcodeInput = barcode;
        }
        #endregion

        #region  Конструктор

        public BarCodeChecker(List<Position> positions)
        {
            BarCodePositions =
                positions.SelectMany(position => position.BoxInfos.SelectMany(info => info.AmcList)
                        .Select(
                            amc =>
                                new BarcodePosition
                                {
                                    Identity = position.Identity,
                                    FullName = position.FullName,
                                    AlcoCode = position.AlcoCode,
                                    InformBRegId = position.InformBRegId,
                                    Barcode = amc.Barcode
                                }))
                    .ToList();
            State = IdleState;
        }
        #endregion

        #region Публичные свойства

        public List<BarcodePosition> BarCodePositions { get; set; }

        public int Differences
        {
            get
            {
                return BarCodePositions.Count -
                       BarCodePositions.Count(
                           position =>
                                   position.Barcode == position.BarcodeInput);
            }
        }
        #endregion

        #region Публичные методы
        public void AddInputBarcode(string barcode)
        {
            if (State != CheckRunningState)
                throw new Exception($"Wrong checker state - {State}");
            if (BarCodePositions.Any(position => position.BarcodeInput == barcode))
            {
                SystemSounds.Beep.Play();
                throw new Exception("Штрихкод уже отсканирован!");
            }
            DistributeBarcode(barcode);
        }
        
        public void StartCheck()
        {
            State = CheckRunningState;
        }

        public void StopCheck()
        {
            State = IdleState;
        }

        public void RemoveBarcode(string barcode)
        {
            var index = BarCodePositions.FindIndex(position => position.BarcodeInput == barcode);
            if (index != -1)
            {
                if (string.IsNullOrEmpty(BarCodePositions[index].Barcode))
                    BarCodePositions.RemoveAt(index);
                else
                    BarCodePositions[index].BarcodeInput = string.Empty;
            }
        }
        #endregion
    }
}