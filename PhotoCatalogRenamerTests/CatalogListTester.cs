using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoCatalogRenamerTests
{
    [TestClass]
    public class CatalogListTester
    {
        /*struct TestedCatalogData
        {
            public string CatalogNameFrom;
            public string CatalogNameTo;
        }
        TestedCatalogData[] testDataArray = { 
                                                new TestedCatalogData() {CatalogNameFrom = "01011977-02022000-День рождения Алисы-1", CatalogNameTo = "1977.01.01-2000.02.02 День рождения Алисы-1" }, 
                                                new TestedCatalogData() {CatalogNameFrom = "01011977_День рождения Алисы", CatalogNameTo = "1977.01.01 День рождения Алисы" }, 
                                                new TestedCatalogData() {CatalogNameFrom = "Произвольное название без даты", CatalogNameTo = "Произвольное название без даты" }, 
                                                new TestedCatalogData() {CatalogNameFrom = "Произвольное название с датой 03031998 в середине текст", CatalogNameTo = "1998.03.03 Произвольное название с датой в середине текст" }, 
                                                new TestedCatalogData() {CatalogNameFrom = "04.04.2004 корректный формат", CatalogNameTo = "04.04.2004 корректный формат" }, 
                                                new TestedCatalogData() {CatalogNameFrom = "корректный формат наоборот 04.04.2004", CatalogNameTo = "04.04.2004 корректный формат наоборот" }, 
                                                new TestedCatalogData() {CatalogNameFrom = "октябрь2006 пример прописью", CatalogNameTo = "2006.10.01 пример прописью" }, 
                                            };*/
        string testedCatalogName1 = "01011977_День рождения Алисы";
        string testedCatalogName2 = "01011977-02022000-День рождения Алисы-1";
        [TestMethod]
        public void tryConvertToDate_foundFormat1()
        {
            PhotoCatalogRenamer.StringFormatter formatter = new PhotoCatalogRenamer.StringFormatter();
            DateTime dt = formatter.tryConvertToDate("01012000");
            Assert.AreEqual(dt.Year, 2000);
            Assert.AreEqual(dt.Month, 1);
            Assert.AreEqual(dt.Day, 1);
        }
        [TestMethod]
        public void tryConvertToDate_foundFormat2()
        {
            PhotoCatalogRenamer.StringFormatter formatter = new PhotoCatalogRenamer.StringFormatter();
            DateTime dt = formatter.tryConvertToDate("01_01_2000");
            Assert.AreEqual(dt.Year, 2000);
            Assert.AreEqual(dt.Month, 1);
            Assert.AreEqual(dt.Day, 1);
        }
        [TestMethod]
        public void tryConvertToDate_foundFormat3()
        {
            PhotoCatalogRenamer.StringFormatter formatter = new PhotoCatalogRenamer.StringFormatter();
            DateTime dt = formatter.tryConvertToDate("01-01-2000");
            Assert.AreEqual(dt.Year, 2000);
            Assert.AreEqual(dt.Month, 1);
            Assert.AreEqual(dt.Day, 1);
        }
        [TestMethod]
        public void tryConvertToDate_foundFormat4()
        {
            PhotoCatalogRenamer.StringFormatter formatter = new PhotoCatalogRenamer.StringFormatter();
            DateTime dt = formatter.tryConvertToDate("01-01_2000");
            Assert.AreEqual(dt.Year, 2000);
            Assert.AreEqual(dt.Month, 1);
            Assert.AreEqual(dt.Day, 1);
        }
        [TestMethod]
        public void tryConvertToDate_foundFormat5()
        {
            PhotoCatalogRenamer.StringFormatter formatter = new PhotoCatalogRenamer.StringFormatter();
            DateTime dt = formatter.tryConvertToDate("01_01-2000");
            Assert.AreEqual(dt.Year, 2000);
            Assert.AreEqual(dt.Month, 1);
            Assert.AreEqual(dt.Day, 1);
        }
        [TestMethod]
        public void CatalogMustBeRenamed_Success()
        {
            PhotoCatalogRenamer.CatalogList cl = new PhotoCatalogRenamer.CatalogList();
            bool result = cl.CatalogMustBeRenamed(new char[]{'_','-'}, "010101-11111");
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void GetNewCatalog_Format1ok()
        {
            PhotoCatalogRenamer.CatalogList cl = new PhotoCatalogRenamer.CatalogList();
            var result = cl.GetNewCatalog(testedCatalogName1, '-');
            Assert.AreEqual(result.catalogDate.Year, 1977);
        }
        [TestMethod]
        public void GetNewCatalog_Format1error()
        {
            PhotoCatalogRenamer.CatalogList cl = new PhotoCatalogRenamer.CatalogList();
            var result = cl.GetNewCatalog(testedCatalogName1, '-');
            Assert.AreEqual(result.catalogDate.Year, 1);
        }
        [TestMethod]
        public void GetNewCatalog_Format2ok()
        {
            PhotoCatalogRenamer.CatalogList cl = new PhotoCatalogRenamer.CatalogList();
            var result = cl.GetNewCatalog(testedCatalogName2, '-');
            Assert.AreEqual(result.catalogDate.Year, 2000);
        }
        [TestMethod]
        public void GetNewCatalog_Format2error()
        {
            PhotoCatalogRenamer.CatalogList cl = new PhotoCatalogRenamer.CatalogList();
            var result = cl.GetNewCatalog(testedCatalogName2, '_');
            Assert.AreEqual(result.catalogDate.Year, 1);
        }
        /*[TestMethod]
        public void RenameCatalog_Success()
        {
            PhotoCatalogRenamer.CatalogList cl = new PhotoCatalogRenamer.CatalogList();
            cl.RenameCatalog(new char[]{'_', '-'},
            Assert.AreEqual(result.catalogDate.Year, 1);
        }*/
    }
}
