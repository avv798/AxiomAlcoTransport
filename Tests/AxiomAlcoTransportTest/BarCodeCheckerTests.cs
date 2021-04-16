using System.Collections.Generic;
using Axiom.AlcoTransport.Engine.BarcodeCheck;
using NUnit.Framework;

namespace Axiom.AlcoTransport.Test
{
    public class BarCodeCheckerTests
    {
        [Test]
        public void TestBarcocdeChecker()
        {
            var barcodes = new List<string> {"123", "234", "345", "456", "578"};
            var positions = new List<Position>
            {
                new Position
                {
                    AlcoCode = "1",
                    Identity = "1",
                    FullName = "Вино",
                    FormBRegId = "Formb1"
                },
                new Position
                {
                    AlcoCode = "2",
                    Identity = "2",
                    FullName = "Водка",
                    FormBRegId = "Formb2"
                }
            };
            positions[0].BoxInfos.AddRange(
                new List<BoxInfo>
                {
                    new BoxInfo
                    {
                        BoxNumber = "box1",
                        AmcList = new List<Amc> {new Amc {Barcode = barcodes[0]}, new Amc {Barcode = barcodes[1]}}
                    },
                    new BoxInfo
                    {
                        BoxNumber = "box2",
                        AmcList = new List<Amc> {new Amc {Barcode = barcodes[2]}}
                    }
                }
            );
            positions[1].BoxInfos.AddRange(
                new List<BoxInfo>
                {
                    new BoxInfo
                    {
                        BoxNumber = "box3",
                        AmcList = new List<Amc> {new Amc {Barcode = barcodes[3]}}
                    },
                    new BoxInfo
                    {
                        BoxNumber = "box4",
                        AmcList = new List<Amc> {new Amc {Barcode = barcodes[4]}}
                    }
                }
            );
            var barcodeChecker = new BarCodeChecker(positions);
            barcodeChecker.StartCheck();
            Assert.AreEqual(5, barcodeChecker.Differences);
            barcodes.ForEach(barcode => barcodeChecker.AddInputBarcode(barcode));
            Assert.AreEqual(0, barcodeChecker.Differences);
            barcodeChecker.AddInputBarcode("777");
            Assert.AreEqual(1, barcodeChecker.Differences);
        }
    }
}