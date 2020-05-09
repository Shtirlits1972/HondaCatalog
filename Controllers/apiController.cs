using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HondaCatalog.Models;
using HondaCatalog.Models.Dto;
using Newtonsoft.Json;

namespace HondaCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiController : Controller
    {
        #region Start
        [Route("/api/GetStarts")]
        public IActionResult GetStarts(int nsalgnp)
        {
            List<start> list = ClassCrud.GetStarts(nsalgnp);
            return Json(list);
        }

        [Route("/api/GetListUnits")]
        public IActionResult GetListUnits(int clangjap)
        {
            List<int> list = ClassCrud.GetListUnits(clangjap);
            return Json(list);
        }

        //       public static List<string> GetListUnits2(int nsalgnp)
        [Route("/api/GetListUnits2")]
        public IActionResult GetListUnits2(int nsalgnp)
        {
            List<string> list = ClassCrud.GetListUnits2(nsalgnp);
            return Json(list);
        }
        //  List<help> GetHelp(int nsalgnp)
        [Route("/api/GetHelp")]
        public IActionResult GetHelp(int nsalgnp)
        {
            List<help> list = ClassCrud.GetHelp(nsalgnp);
            return Json(list);
        }
        //----------------------------
        //List<language> GetLanguages(string language_id)
        [Route("/api/GetLanguages")]
        public IActionResult GetLanguages(string language_id)
        {
            List<language> list = ClassCrud.GetLanguages(language_id);
            return Json(list);
        }

        //List<string> GetLanguagesDistinct()
        [Route("/api/GetLanguagesDistinct")]
        public IActionResult GetLanguagesDistinct()
        {
            List<string> list = ClassCrud.GetLanguagesDistinct();
            return Json(list);
        }
        #endregion

        #region Модели и блоки
        //  список моделей
        //  List<ModelAvto> GetModelAvtos(int ngnp)
        [Route("/api/GetModelAvtos")]
        public IActionResult GetModelAvtos(int ngnp)
        {
            List<ModelAvto> list = ClassCrud.GetModelAvtos(ngnp);
            return Json(list);
        }
        //  к-во дверей
        //  List<int> GetNumberDoors(string modelName, int ngnp)
        [Route("/api/GetNumberDoors")]
        public IActionResult GetNumberDoors(string modelName, int ngnp)
        {
            List<int> list = ClassCrud.GetNumberDoors(modelName, ngnp);
            return Json(list);
        }

        //  List<string> GetYearIssue(int xcardrs, string cmodnamepc)
        //  Год выпуска
        [Route("/api/GetYearIssue")]
        public IActionResult GetYearIssue(int xcardrs, string cmodnamepc)
        {
            List<string> list = ClassCrud.GetYearIssue(xcardrs, cmodnamepc);
            return Json(list);
        }

        //Список кодов типов моделей
        //List<string> GetTypeModelCodes(int xcardrs, string cmodnamepc, int dmodyr)
        [Route("/api/GetTypeModelCodes")]
        public IActionResult GetTypeModelCodes(int xcardrs, string cmodnamepc, int dmodyr)
        {
            List<string> list = ClassCrud.GetTypeModelCodes(xcardrs, cmodnamepc, dmodyr);
            return Json(list);
        }

        // Список Area для полученных типов моделей
        // List<string> GetArealistForReceivedModelTypes(int ngnp, string hmodtyp)
        [Route("/api/GetArealistForReceivedModelTypes")]
        public IActionResult GetArealistForReceivedModelTypes(int ngnp, string hmodtyp)
        {
            List<string> list = ClassCrud.GetArealistForReceivedModelTypes(ngnp, hmodtyp);
            return Json(list);
        }

        // Список Area для полученных типов моделей (уточнение)
        // public static List<string> GetArealistForReceivedModelTypes2(string cmodnamepc, int xcardrs, int dmodyr, string carea)
        [Route("/api/GetArealistForReceivedModelTypes2")]
        public IActionResult GetArealistForReceivedModelTypes2(string cmodnamepc, int xcardrs, int dmodyr, string carea)
        {
            List<string> list = ClassCrud.GetArealistForReceivedModelTypes2(cmodnamepc, xcardrs, dmodyr, carea);
            return Json(list);
        }

        //   Выбор типов уточненный по Area
        //   public static List<int> GetCheckFor2orMoreEntries(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        [Route("/api/GetCheckFor2orMoreEntries")]
        public IActionResult GetCheckFor2orMoreEntries(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        {
            List<int> list = ClassCrud.GetCheckFor2orMoreEntries(cmodnamepc, dmodyr, xcardrs, carea);
            return Json(list);
        }

        //    Еще проверка типов на cmftrepc
        // public static List<int> GettypeCheckingOnCmftrepc(string cmftrepc, string hmodtyp)
        [Route("/api/GettypeCheckingOnCmftrepc")]
        public IActionResult GettypeCheckingOnCmftrepc(string cmftrepc, string hmodtyp)
        {
            List<int> list = ClassCrud.GettypeCheckingOnCmftrepc(cmftrepc, hmodtyp);
            return Json(list);
        }

        //   Трассировка 'CIVIC AERODECK'
        //   public static List<string> GetTraceCivicAerodeck(string hmodtyp)
        [Route("/api/GetTraceCivicAerodeck")]
        public IActionResult GetTraceCivicAerodeck(string hmodtyp)
        {
            List<string> list = ClassCrud.GetTraceCivicAerodeck(hmodtyp);
            return Json(list);
        }

        //  Определяется класс типа авто по выбранной трансмиссии
        //  public static List<string> GetClassOfCarTypeForTheSelectedTransmission(string hmodtyp)
        [Route("/api/GetClassOfCarTypeForTheSelectedTransmission")]
        public IActionResult GetClassOfCarTypeForTheSelectedTransmission(string hmodtyp)
        {
            List<string> list = ClassCrud.GetClassOfCarTypeForTheSelectedTransmission(hmodtyp);
            return Json(list);
        }

        //   Еще раз список типов
        //  public static List<int> GetTypeList2(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        [Route("/api/GetTypeList2")]
        public IActionResult GetTypeList2(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        {
            List<int> list = ClassCrud.GetTypeList2(cmodnamepc, dmodyr, xcardrs, carea);
            return Json(list);
        }

        //   Информация про модель
        //   public static List<CarModel> GetCarModelInfo(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        [Route("/api/GetCarModelInfo")]
        public IActionResult GetCarModelInfo(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        {
            List<CarModel> list = ClassCrud.GetCarModelInfo(cmodnamepc, dmodyr, xcardrs, carea);
            return Json(list);
        }

        //  странный запрос
        //   public static List<int> GetStrangeRequest(string hmodtyp, string cmftrepc = "")
        [Route("/api/GetStrangeRequest")]
        public IActionResult GetStrangeRequest(string hmodtyp, string cmftrepc)
        {
            List<int> list = ClassCrud.GetStrangeRequest(hmodtyp, cmftrepc);
            return Json(list);
        }

        //  Список Equipment
        //  public static List<Equipment> GetListEquipment(string hmodtyp)
        [Route("/api/GetListEquipment")]
        public IActionResult GetListEquipment(string hmodtyp)
        {
            List<Equipment> list = ClassCrud.GetListEquipment(hmodtyp);
            return Json(list);
        }
        //  Запрос фильтрует типы авто
        // public static List<int> GetFilterTypeAvto(string hmodtyp, string cmnopt)
        [Route("/api/GetFilterTypeAvto")]
        public IActionResult GetFilterTypeAvto(string hmodtyp, string cmnopt)
        {
            List<int> list = ClassCrud.GetFilterTypeAvto(hmodtyp, cmnopt);
            return Json(list);
        }
        //   Затем заново уточняет опции для отфильтрованного списка
        //   public static List<Equipment> GetGetFilterTypeAvto(string hmodtyp)
        [Route("/api/GetFilterTypeAvto2")]
        public IActionResult GetFilterTypeAvto2(string hmodtyp)
        {
            List<Equipment> list = ClassCrud.GetFilterTypeAvto2(hmodtyp);
            return Json(list);
        }

        //  Блоки – поиск по номеру
        // public static List<Block> GetBlocksSearchByNumber(int clangjap, string npl)
        [Route("/api/GetBlocksSearchByNumber")]
        public IActionResult GetBlocksSearchByNumber(int clangjap, string npl)
        {
            List<Block> list = ClassCrud.GetBlocksSearchByNumber(clangjap, npl);
            return Json(list);
        }

        //  Блоки – поиск по описанию 
        //  public static List<Block> GetBlocksSearchByDeskr(int clangjap, string npl)
        [Route("/api/GetBlocksSearchByDeskr")]
        public IActionResult GetBlocksSearchByDeskr(int clangjap, string npl)
        {
            List<Block> list = ClassCrud.GetBlocksSearchByDeskr(clangjap, npl);
            return Json(list);
        }
        #endregion

        #region запчасти
        // Выбор группы запчастей
        // public static List<int> GetSelectionOfSparePartGroup(string npl)
        [Route("/api/GetSelectionOfSparePartGroup")]
        public IActionResult GetSelectionOfSparePartGroup(string npl)
        {
            List<int> list = ClassCrud.GetSelectionOfSparePartGroup(npl);
            return Json(list);
        }

        //  Список крупноузловых групп
        // public static List<LargeGroupSpareParts> GetListLargeGroupSpareParts(string clangjap, string npl, string hmodtyp)
        [Route("/api/GetListLargeGroupSpareParts")]
        public IActionResult GetListLargeGroupSpareParts(string clangjap, string npl, string hmodtyp)
        {
            List<LargeGroupSpareParts> list = ClassCrud.GetListLargeGroupSpareParts(clangjap, npl, hmodtyp);
            return Json(list);
        }

        //  Уточнение типов авто
        // public static List<Grade> GetListGrade(string hmodtyp)
        [Route("/api/GetListGrade")]
        public IActionResult GetListGrade(string hmodtyp)
        {
            List<Grade> list = ClassCrud.GetListGrade(hmodtyp);
            return Json(list);
        }

        //   Отобрать блоки запчастей для выбранной крупноузловой группы "pblokt"."nplgrp"
        //   public static List<PartsBlocks> GetListPartsBlocks(string clangjap, string nplgrp, string npl, string hmodtyp)
        [Route("/api/GetListPartsBlocks")]
        public IActionResult GetListPartsBlocks(string clangjap, string nplgrp, string npl, string hmodtyp)
        {
            List<PartsBlocks> list = ClassCrud.GetListPartsBlocks(clangjap, nplgrp, npl, hmodtyp);
            return Json(list);
        }

        //  Выборка пользовательских примечаний
        //  public static List<notes> GetListNotes(int partslist_number)
        [Route("/api/GetListNotes")]
        public IActionResult GetListNotes(int partslist_number)
        {
            List<notes> list = ClassCrud.GetListNotes(partslist_number);

            if(list == null)
            {
                return Ok("Ошибка в запросе!");
            }

            return Json(list);
        }

        //   Перепроверка типов авто для выбранного блока запчастей
        // public static List<int> GetTypeAvtoForBlockSpareParts(string npl, string nplblk, string hmodtyp)
        [Route("/api/GetTypeAvtoForBlockSpareParts")]
        public IActionResult GetTypeAvtoForBlockSpareParts(string npl, string nplblk, string hmodtyp)
        {
            List<int> list = ClassCrud.GetTypeAvtoForBlockSpareParts(npl, nplblk, hmodtyp);
            return Json(list);
        }

        //  Список запчастей
        //  public static List<SpareParts> GetListSpareParts(string clangjap, string npl, string nplblk, string hmodtyp)
        [Route("/api/GetListSpareParts")]
        public IActionResult GetListSpareParts(string clangjap, string npl, string nplblk, string hmodtyp)
        {
            List<SpareParts> list = ClassCrud.GetListSpareParts(clangjap, npl, nplblk, hmodtyp);
            return Json(list);
        }
        #endregion

        #region цены
        //   Поиск номера по позиции на рисунке
        //  public static List<int> GetSearchForNumberByPositionInFigure(string npl, string illustrationnumber, int x, int y)
        [Route("/api/GetSearchForNumberByPositionInFigure")]
        public IActionResult GetSearchForNumberByPositionInFigure(string npl, string illustrationnumber, int x, int y)
        {
            List<int> list = ClassCrud.GetSearchForNumberByPositionInFigure(npl, illustrationnumber, x, y);
            return Json(list);
        }

        // public static List<int> GetTypeAvtoSelectedSpareParts(int clangjap, string npl, string nplblk, int nplpartref, string npartgenu)
        [Route("/api/GetTypeAvtoSelectedSpareParts")]
        public IActionResult GetTypeAvtoSelectedSpareParts(int clangjap, string npl, string nplblk, int nplpartref, string npartgenu)
        {
            List<int> list = ClassCrud.GetTypeAvtoSelectedSpareParts(clangjap, npl, nplblk, nplpartref, npartgenu);
            return Json(list);
        }

        //  После нажатия на Ок
        // Непонятно  ????
        // не работает - нет таблицы car_info
        // public static List<car_info> GetCarInfo(string vin)
        [Route("/api/GetCarInfo")]
        public IActionResult GetCarInfo(string vin)
        {
            List<car_info> list = ClassCrud.GetCarInfo(vin);
            return Json(list);
        }

        // Непонятно
        // не работает - нет таблицы  job_frt
        // public static List<job_frt> GetJobFrt(string job_id)
        [Route("/api/GetJobFrt")]
        public IActionResult GetJobFrt(string job_id)
        {
            List<job_frt> list = ClassCrud.GetJobFrt(job_id);
            return Json(list);
        }

        //   Получить цену на выбранную запчасть
        //  public static List<decimal> GetTypeAvtoSelectedSpareParts(string npartgenu)
        [Route("/api/GetTypeAvtoSelectedSpareParts2")]
        public IActionResult GetTypeAvtoSelectedSpareParts2(string npartgenu)
        {
            List<decimal> list = ClassCrud.GetTypeAvtoSelectedSpareParts2(npartgenu);
            return Json(list);
        }
        #endregion

        #region запчасти-2
        //  Существую ли запчасти - количество
        //  public static int GetQtySpareParts(int clangjap, string npartgenu)
        [Route("/api/GetQtySpareParts")]
        public IActionResult GetQtySpareParts(int clangjap, string npartgenu)
        {
            int result = ClassCrud.GetQtySpareParts(clangjap, npartgenu);
            return Ok(result);
        }


        //  Найти запчасти
        //  public static List<SpareParts2> GetFindSpareParts(int clangjap, string npartgenu)
        [Route("/api/GetFindSpareParts")]
        public IActionResult GetFindSpareParts(int clangjap, string npartgenu)
        {
            List<SpareParts2> list = ClassCrud.GetFindSpareParts(clangjap, npartgenu);
            return Json(list);
        }


        // Part Description
        //  public static int GetQtyPartDescription(int clangjap, string xpartext)
        [Route("/api/GetQtyPartDescription")]
        public IActionResult GetQtyPartDescription(int clangjap, string xpartext)
        {
            int result = ClassCrud.GetQtyPartDescription(clangjap, xpartext);
            return Ok(result);
        }


        // Непонятно
        // получаем количество чего-то
        // public static List<int> GetNser(int nsalgnp)
        [Route("/api/GetNser")]
        public IActionResult GetNser(int nsalgnp)
        {
            List<int> list = ClassCrud.GetNser(nsalgnp);
            return Json(list);
        }
        #endregion

        #region VIN
        //  Список авто, к которым подходит выбранная запасть
        // Не работает, нет таблицы vw_blockpartsmodeltypes  - ????????
        //  public static List<Avto> GetAvtoList(string npartgenu, int clangjap)
        [Route("/api/GetAvtoList")]
        public IActionResult GetAvtoList(string npartgenu, int clangjap)
        {
            List<Avto> list = ClassCrud.GetAvtoList(npartgenu, clangjap);
            return Json(list);
        }

        //  Определить Информация про модель
        // public static List<ModelInfo> GetModelInfoList(string cmodnamepc, int dmodyr, int xcardrs, string carea, string cmftrepc)
        [Route("/api/GetModelInfoList")]
        public IActionResult GetModelInfoList(string cmodnamepc, int dmodyr, int xcardrs, string carea, string cmftrepc)
        {
            List<ModelInfo> list = ClassCrud.GetModelInfoList(cmodnamepc, dmodyr, xcardrs, carea, cmftrepc);
            return Json(list);
        }

        //  Список типов по выбранным параметрам
        // public static List<int> GetListTypes(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        [Route("/api/GetListTypes")]
        public IActionResult GetListTypes(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        {
            List<int> list = ClassCrud.GetListTypes(cmodnamepc, dmodyr, xcardrs, carea);
            return Json(list);
        }

        //Список типов по выбранным параметрам - 2
        // public static List<int> GetListTypesByParam(string NPL)
        [Route("/api/GetListTypesByParam")]
        public IActionResult GetListTypesByParam(string NPL)
        {
            List<int> list = ClassCrud.GetListTypesByParam(NPL);
            return Json(list);
        }

        //  Список блоков по выбранным параметрам авто Block3
        //  public static List<Block3> GetListBlock3ByParamAvto(string clangjap, string npl, string hmodtyp)
        [Route("/api/GetListBlock3ByParamAvto")]
        public IActionResult GetListBlock3ByParamAvto(string clangjap, string npl, string hmodtyp)
        {
            List<Block3> list = ClassCrud.GetListBlock3ByParamAvto(clangjap, npl, hmodtyp);
            return Json(list);
        }

        //  Отобрать только те блоки в которых будет искомая запчасть для авто выбранного по параметрам
        //  public static List<Block4> GetListBlock4ByParamAvto(string npl, string npartgenu, string clangjap, string hmodtyp)
        [Route("/api/GetListBlock4ByParamAvto")]
        public IActionResult GetListBlock4ByParamAvto(string npl, string npartgenu, string clangjap, string hmodtyp)
        {
            List<Block4> list = ClassCrud.GetListBlock4ByParamAvto(npl, npartgenu, clangjap, hmodtyp);
            return Json(list);
        }

        //  Проверка типов
        //  public static List<int> GetCheckTypes(string npl, string nplblk, string hmodtyp)
        [Route("/api/GetCheckTypes")]
        public IActionResult GetCheckTypes(string npl, string nplblk, string hmodtyp)
        {
            List<int> list = ClassCrud.GetCheckTypes(npl, nplblk, hmodtyp);
            return Json(list);
        }

        // Список запчастей
        // public static List<SpareParts> GetListSpareParts2(string clangjap, string npl, string nplblk, string hmodtyp)
        [Route("/api/GetListSpareParts2")]
        public IActionResult GetListSpareParts2(string clangjap, string npl, string nplblk, string hmodtyp)
        {
            List<SpareParts> list = ClassCrud.GetListSpareParts2(clangjap, npl, nplblk, hmodtyp);
            return Json(list);
        }

        //  VIN разборка
        //public static List<int> GetTypesByVin(string nfrmpf, string nfrmseqepcstrt, string nfrmseqepcend)
        [Route("/api/GetTypesByVin")]
        public IActionResult GetTypesByVin(string nfrmpf, string nfrmseqepcstrt, string nfrmseqepcend)
        {
            List<int> list = ClassCrud.GetTypesByVin(nfrmpf, nfrmseqepcstrt, nfrmseqepcend);
            return Json(list);
        }

        //Получение списка Area, список для VIN - SHHCG7540YU003985
        // public static List<string> GetAreaList(string hmodtyp, string ngnp)
        [Route("/api/GetAreaList")]
        public IActionResult GetAreaList(string hmodtyp, string ngnp)
        {
            List<string> list = ClassCrud.GetAreaList(hmodtyp, ngnp);
            return Json(list);
        }

        //  Информация о типе авто
        // public static List<CarTypeInfo> GetListCarTypeInfo(string nfrmpf, string nfrmseqepcstrt, string nfrmseqepcend, string carea)
        [Route("/api/GetListCarTypeInfo")]
        public IActionResult GetListCarTypeInfo(string nfrmpf, string nfrmseqepcstrt, string nfrmseqepcend, string carea)
        {
            List<CarTypeInfo> list = ClassCrud.GetListCarTypeInfo(nfrmpf, nfrmseqepcstrt, nfrmseqepcend, carea);
            return Json(list);
        }

        //   Информация про модель
        // public static List<CarModel> GetCarModelInfo2(string cmodnamepc, int dmodyr, int xcardrs, string carea, string cmftrepc)
        [Route("/api/GetCarModelInfo2")]
        public IActionResult GetCarModelInfo2(string cmodnamepc, int dmodyr, int xcardrs, string carea, string cmftrepc)
        {
            List<CarModel> list = ClassCrud.GetCarModelInfo2(cmodnamepc, dmodyr, xcardrs, carea, cmftrepc);
            return Json(list);
        } 
        #endregion
    }
}