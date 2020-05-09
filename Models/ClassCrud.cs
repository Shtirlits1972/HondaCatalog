using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using HondaCatalog.Models.Dto;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace HondaCatalog.Models
{
    public class ClassCrud
    {
        private static string strConn = Ut.GetMySQLConnect();

        #region Start
        public static List<start> GetStarts(int nsalgnp) //= 201
        {
            List<start> list = new List<start>();

            string strCommand = "SELECT rexcheuroown, ccurrypreeuro, hcurrydec, nser  FROM psagpt WHERE nsalgnp = @nsalgnp; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<start>(strCommand, new { nsalgnp }).ToList();
            }

            return list;
        }
        public static List<int> GetListUnits(int clangjap)
        {
            List<int> list = new List<int>();

            string strCommand = "SELECT pgrout.clangjap FROM pgrout WHERE(pgrout.clangjap = @clangjap); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { clangjap }).ToList();
            }

            return list;
        }
        public static List<string> GetListUnits2(int nsalgnp)
        {
            List<string> list = new List<string>();

            string strCommand = "SELECT ccurry FROM psagpt WHERE nsalgnp = @nsalgnp; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand, new { nsalgnp }).ToList();
            }

            return list;
        }
        public static List<help> GetHelp(int nsalgnp)
        {
            List<help> list = new List<help>();

            string strCommand = "SELECT help.language_code, help.help_context_id, help.window_title FROM help; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<help>(strCommand, new { nsalgnp }).ToList();
            }

            return list;
        }
        public static List<language> GetLanguages(string language_id)
        {
            List<language> list = new List<language>();

            string strCommand = "SELECT  dyn_object.id , " +
                      " dyn_object.language_id ,  " +
                      " dyn_object.object , " +
                      " dyn_object.attribut ,  " +
                      " dyn_object.translationname " +
                      " FROM dyn_object " +
                      " WHERE(dyn_object.language_id = @language_id); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<language>(strCommand, new { language_id }).ToList();
            }

            return list;
        }
        public static List<string> GetLanguagesDistinct()
        {
            List<string> list = new List<string>();

            string strCommand = "SELECT DISTINCT pgrout.clangjap FROM pgrout ORDER BY pgrout.clangjap ASC; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand).ToList();
            }

            return list;
        }
        #endregion

        #region Модели и блоки
        //  список моделей
        public static List<ModelAvto> GetModelAvtos(int ngnp)
        {
            List<ModelAvto> list = new List<ModelAvto>();

            string strCommand = "SELECT DISTINCT pmodlt.cmodnamepc, pmdldt.ngnp " +
                          " FROM pmodlt, pmdldt " +
                          " WHERE( " +
                          "         NOT EXISTS(SELECT 1 " +
                          "                       FROM pmdldt " +
                          "                      WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                          "                        AND pmdldt.dmodyr = pmodlt.dmodyr " +
                          "                        AND pmdldt.xcardrs = pmodlt.xcardrs " +
                          "                        AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                          "                        AND pmdldt.ngnp = @ngnp " +
                          "                    ) " +
                          "         AND pmodlt.dmodlnch < now() " +
                          "       ) " +
                          "    OR( " +
                          "             EXISTS(SELECT 1 " +
                          "                   FROM pmdldt " +
                          "                  WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                          "                    AND pmdldt.dmodyr = pmodlt.dmodyr " +
                          "                    AND pmdldt.xcardrs = pmodlt.xcardrs " +
                          "                    AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                          "                    AND pmdldt.ngnp = @ngnp " +
                          "                    AND pmdldt.dmodlnch < now() " +
                          "                ) " +
                          "      )";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<ModelAvto>(strCommand, new { ngnp }).ToList();
            }


            return list;
        }
        //  к-во дверей
        public static List<int> GetNumberDoors(string modelName, int ngnp)
        {
            List<int> list = new List<int>();

            string strCommand = "SELECT DISTINCT pmodlt.xcardrs " +
              "   FROM pmodlt, pmdldt " +
              "  WHERE pmodlt.cmodnamepc = @modelName  " +
              "    AND( " +
              "        ( " +
              "         NOT EXISTS(SELECT 1 " +
              "                       FROM pmdldt " +
              "                      WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
              "                        AND pmdldt.dmodyr = pmodlt.dmodyr " +
              "                        AND pmdldt.xcardrs = pmodlt.xcardrs " +
              "                        AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
              "                        AND pmdldt.ngnp = @ngnp " +
              "                    ) " +
              "         AND pmodlt.dmodlnch < now() " +
              "       ) " +
              "    OR( " +
              "             EXISTS(SELECT 1 " +
              "                   FROM pmdldt " +
              "                  WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
              "                    AND pmdldt.dmodyr = pmodlt.dmodyr " +
              "                    AND pmdldt.xcardrs = pmodlt.xcardrs " +
              "                    AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
              "                    AND pmdldt.ngnp = @ngnp " +
              "                    AND pmdldt.dmodlnch < now() " +
              "                ) " +
              "       ) " +
              "       ) " +
              " ORDER BY pmodlt.xcardrs ASC ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { modelName, ngnp }).ToList();
            }
            return list;
        }
        //  Год выпуска
        public static List<string> GetYearIssue(int xcardrs, string cmodnamepc)
        {
            List<string> list = new List<string>();

            string strCommand = "SELECT DISTINCT pmodlt.dmodyr " +
                        "        FROM pmodlt, pmdldt " +
                        "        WHERE pmodlt.cmodnamepc = @cmodnamepc " +
                        "         AND pmodlt.xcardrs = @xcardrs " +
                        "         AND((   " +
                        "               NOT EXISTS(SELECT 1 " +
                        "                             FROM pmdldt " +
                        "                            WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                        "                              AND pmdldt.dmodyr = pmodlt.dmodyr " +
                        "                              AND pmdldt.xcardrs = pmodlt.xcardrs " +
                        "                              AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                        "                          ) " +
                        "               AND pmodlt.dmodlnch < now() " +
                        "             ) " +
                        "          OR( " +
                        "                   EXISTS(SELECT 1 " +
                        "                         FROM pmdldt " +
                        "                        WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                        "                          AND pmdldt.dmodyr = pmodlt.dmodyr " +
                        "                          AND pmdldt.xcardrs = pmodlt.xcardrs " +
                        "                          AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                        "                          AND pmdldt.dmodlnch < now() " +
                        "                      ) " +
                        "             ) " +
                        "             ) " +
                        "       ORDER BY pmodlt.dmodyr ASC; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand, new { cmodnamepc, xcardrs }).ToList();
            }

            return list;
        }

        //Список кодов типов моделей
        public static List<string> GetTypeModelCodes(int xcardrs, string cmodnamepc, int dmodyr)
        {
            List<string> list = new List<string>();

            string strCommand = "SELECT DISTINCT  pmotyt.hmodtyp " +
                                " FROM pmotyt " +
                                " WHERE(pmotyt.cmodnamepc = @cmodnamepc) " +
                                "   and(pmotyt.dmodyr = @dmodyr) " +
                                "   and(pmotyt.xcardrs = @xcardrs); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand, new { cmodnamepc, dmodyr, xcardrs }).ToList();
            }

            return list;
        }
        // Список Area для полученных типов моделей
        public static List<string> GetArealistForReceivedModelTypes(int ngnp, string hmodtyp)
        {
            List<string> list = new List<string>();

            string strCommand = "SELECT DISTINCT pmotyt.carea " +
                      "  FROM pmotyt LEFT OUTER JOIN pmodlt ON(pmotyt.cmodnamepc = pmodlt.cmodnamepc AND " +
                      "                                              pmotyt.dmodyr = pmodlt.dmodyr AND " +
                      "                                              pmotyt.xcardrs = pmodlt.xcardrs) " +
                      "       ,pmdldt " +
                      " WHERE pmotyt.hmodtyp in (@hmodtyp) " +
                      "   AND( " +
                      "       ( " +
                      "         NOT EXISTS(SELECT 1 " +
                      "                       FROM pmdldt " +
                      "                      WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                      "                        AND pmdldt.dmodyr = pmodlt.dmodyr " +
                      "                        AND pmdldt.xcardrs = pmodlt.xcardrs " +
                      "                        AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                      "                        AND pmdldt.ngnp = @ngnp " +
                      "                    ) " +
                      "         AND pmodlt.dmodlnch < now() " +
                      "       ) " +
                      "    OR( " +
                      "             EXISTS(SELECT 1 " +
                      "                   FROM pmdldt " +
                      "                  WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                      "                    AND pmdldt.dmodyr = pmodlt.dmodyr " +
                      "                    AND pmdldt.xcardrs = pmodlt.xcardrs " +
                      "                    AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                      "                    AND pmdldt.ngnp = @ngnp " +
                      "                    AND pmdldt.dmodlnch < now() " +
                      "                ) " +
                      "       ) " +
                      "       )";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand, new { hmodtyp, ngnp }).ToList();
            }

            return list;
        }
        // Список Area для полученных типов моделей (уточнение)
        public static List<string> GetArealistForReceivedModelTypes2(string cmodnamepc, int xcardrs, int dmodyr, string carea)
        {
            List<string> list = new List<string>();

            string strCommand = "SELECT DISTINCT pmodlt.cmftrepc  " +
                              "  FROM pmodlt JOIN pmotyt ON(pmodlt.cmodnamepc = pmotyt.cmodnamepc AND " +
                              "                                   pmodlt.dmodyr = pmotyt.dmodyr AND " +
                              "                                   pmodlt.xcardrs = pmotyt.xcardrs) " +
                              "       ,pmdldt " +
                              " WHERE pmodlt.cmodnamepc = 'ACCORD' " +
                              "   AND pmodlt.xcardrs = @xcardrs " +
                              "   AND pmodlt.dmodyr = @dmodyr " +
                              "   AND pmotyt.carea = @carea " +
                              "   AND( " +
                              "       ( " +
                              "         NOT EXISTS(SELECT 1 " +
                              "                       FROM pmdldt " +
                              "                      WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                              "                        AND pmdldt.dmodyr = pmodlt.dmodyr " +
                              "                        AND pmdldt.xcardrs = pmodlt.xcardrs " +
                              "                        AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                              "                    ) " +
                              "         AND pmodlt.dmodlnch < now() " +
                              "       ) " +
                              "    OR( " +
                              "             EXISTS(SELECT 1 " +
                              "                   FROM pmdldt " +
                              "                  WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                              "                    AND pmdldt.dmodyr = pmodlt.dmodyr " +
                              "                    AND pmdldt.xcardrs = pmodlt.xcardrs " +
                              "                    AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                              "                    AND pmdldt.dmodlnch < now() " +
                              "                ) " +
                              "       ) " +
                              "       )";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand, new { cmodnamepc, xcardrs, dmodyr, carea }).ToList();
            }

            return list;
        }
        //   Выбор типов уточненный по Area
        public static List<int> GetCheckFor2orMoreEntries(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT DISTINCT  pmotyt.hmodtyp " +
                                " FROM pmotyt " +
                                " WHERE(pmotyt.cmodnamepc = @cmodnamepc) " +
                                " and(pmotyt.dmodyr = @dmodyr) " +
                                " and(pmotyt.xcardrs = @xcardrs) " +
                                " and(pmotyt.carea = @carea)  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { cmodnamepc, dmodyr, xcardrs, carea }).ToList();
            }
            return list;
        }
        //    Еще проверка типов на cmftrepc
        public static List<int> GettypeCheckingOnCmftrepc(string cmftrepc, string hmodtyp)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT  pmotyt.hmodtyp      " +
                                " FROM pmotyt " +
                                " WHERE ( pmotyt.cmftrepc = @cmftrepc ) " +
                                $" and ( pmotyt.hmodtyp in ( { hmodtyp } )); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { cmftrepc }).ToList();
            }
            return list;
        }
        //   Трассировка 'CIVIC AERODECK'
        public static List<string> GetTraceCivicAerodeck(string hmodtyp)
        {
            List<string> list = new List<string>();

            string strCommand = " SELECT DISTINCT pmotyt.ctrsmtyp " +
                                " FROM pmotyt " +
                                " WHERE pmotyt.xgradefulnam between '00' and 'ZZ' " +
                                $" AND pmotyt.hmodtyp in ({ hmodtyp }) " +
                                " ORDER BY pmotyt.ctrsmtyp ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand, new { hmodtyp }).ToList();
            }

            return list;
        }
        //  Определяется класс типа авто по выбранной трансмиссии
        public static List<string> GetClassOfCarTypeForTheSelectedTransmission(string hmodtyp)
        {
            List<string> list = new List<string>();

            string strCommand = " SELECT DISTINCT pmotyt.xgradefulnam  " +
                                "     FROM pmotyt " +
                                "     WHERE pmotyt.ctrsmtyp between '00' and 'ZZ' " +
                                "     AND pmotyt.hmodtyp in (@hmodtyp) " +
                                " ORDER BY pmotyt.xgradefulnam; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand, new { hmodtyp }).ToList();
            }

            return list;
        }
        //   Еще раз список типов
        public static List<int> GetTypeList2(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT DISTINCT  pmotyt.hmodtyp " +
                                " FROM pmotyt " +
                                " WHERE(pmotyt.cmodnamepc = @cmodnamepc) " +
                                " and(pmotyt.dmodyr = @dmodyr) " +
                                " and(pmotyt.xcardrs = @xcardrs) " +
                                " and(pmotyt.carea = @carea) ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { cmodnamepc, dmodyr, xcardrs, carea }).ToList();
            }
            return list;
        }
        //   Информация про модель
        public static List<CarModel> GetCarModelInfo(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        {
            List<CarModel> list = new List<CarModel>();

            string strCommand = " SELECT DISTINCT pmodlt.nphtmodbas,  " +
                        " 				pmodlt.nphtmodmodif,    " +
                        " 				pmotyt.npl, " +
                        " 				pmotyt.cmftrepc " +
                        "        FROM pmodlt JOIN pmotyt ON pmodlt.cmodnamepc = pmotyt.cmodnamepc AND " +
                        "                                         pmodlt.xcardrs = pmotyt.xcardrs AND " +
                        "                                                   pmodlt.dmodyr = pmotyt.dmodyr AND " +
                        "                                                   pmodlt.cmftrepc = pmotyt.cmftrepc " +
                        "       WHERE(pmodlt.cmodnamepc = @cmodnamepc) " +
                        "          AND(pmodlt.dmodyr = @dmodyr) " +
                        "         AND(pmodlt.xcardrs = @xcardrs) " +
                        "           AND(pmotyt.carea = @carea) " +
                        "         AND( " +
                        "             ( " +
                        "               NOT EXISTS(SELECT 1 " +
                        "                             FROM pmdldt " +
                        "                            WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                        "                              AND pmdldt.dmodyr = pmodlt.dmodyr " +
                        "                              AND pmdldt.xcardrs = pmodlt.xcardrs " +
                        "                              AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                        "                          ) " +
                        "               AND pmodlt.dmodlnch < now() " +
                        "             ) " +
                        "          OR( " +
                        "                   EXISTS(SELECT 1 " +
                        "                         FROM pmdldt " +
                        "                        WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                        "                          AND pmdldt.dmodyr = pmodlt.dmodyr " +
                        "                          AND pmdldt.xcardrs = pmodlt.xcardrs " +
                        "                          AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                        "                          AND pmdldt.dmodlnch < now() " +
                        "                      ))) ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<CarModel>(strCommand, new { cmodnamepc, dmodyr, xcardrs, carea }).ToList();
            }
            return list;
        }
        //  странный запрос
        public static List<int> GetStrangeRequest(string hmodtyp, string cmftrepc)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT  pmotyt.hmodtyp  " +
                                " FROM pmotyt  " +
                                " WHERE(pmotyt.cmftrepc = @cmftrepc)  " +
                                $" and(pmotyt.hmodtyp in ({ hmodtyp }))  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { cmftrepc }).ToList();
            }
            return list;
        }
        //  Список Equipment
        public static List<Equipment> GetListEquipment(string hmodtyp)
        {
            List<Equipment> list = new List<Equipment>();

            string strCommand = " SELECT distinct 0 as selected, " +
                                " pmtmot.cmnopt " +
                                " FROM pmotyt JOIN pmtmot ON pmotyt.hmodtyp = pmtmot.hmodtyp " +
                                $" WHERE pmtmot.hmodtyp In({ hmodtyp }) " +
                                " ORDER BY pmtmot.cmnopt  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<Equipment>(strCommand).ToList();
            }
            return list;
        }
        //  Запрос фильтрует типы авто
        public static List<int> GetFilterTypeAvto(string hmodtyp, string cmnopt)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT DISTINCT pmtmot.hmodtyp   " +
                                "  FROM pmtmot " +
                                $" WHERE(pmtmot.hmodtyp IN ( {hmodtyp} )) AND " +
                                "   (pmtmot.cmnopt IN (@cmnopt))  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { cmnopt }).ToList();
            }
            return list;
        }
        //   Затем заново уточняет опции для отфильтрованного списка
        public static List<Equipment> GetFilterTypeAvto2(string hmodtyp)
        {
            List<Equipment> list = new List<Equipment>();

            string strCommand = " SELECT distinct 0 as selected,  " +
                                " pmtmot.cmnopt " +
                                " FROM pmotyt JOIN pmtmot ON pmotyt.hmodtyp = pmtmot.hmodtyp " +
                                $" WHERE pmtmot.hmodtyp In({hmodtyp}) " +
                                " ORDER BY pmtmot.cmnopt  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<Equipment>(strCommand).ToList();
            }
            return list;
        }
        //  Блоки – поиск по номеру
        public static List<Block> GetBlocksSearchByNumber(int clangjap, string npl)
        {
            List<Block> list = new List<Block>();

            string strCommand = " SELECT pbldst.xplblk, pblokt.nplblkedit, pblokt.nplblk " +
                                " FROM pbldst, pblokt " +
                                " WHERE(pbldst.npl = pblokt.npl) " +
                                "  and(pbldst.nplblk = pblokt.nplblk) " +
                                "  and(pbldst.clangjap = @clangjap) " +
                                "  and(pbldst.npl = @npl) " +
                                "  ORDER BY  " +
                                "   pblokt.nplblkedit  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<Block>(strCommand, new { clangjap, npl }).ToList();
            }
            return list;
        }
        //  Блоки – поиск по описанию 
        public static List<Block> GetBlocksSearchByDeskr(int clangjap, string npl)
        {
            List<Block> list = new List<Block>();

            string strCommand = " SELECT  pbldst.xplblk , left (pbldst.xplblk, 12) as short_descr,  pblokt.nplblkedit " +
                                " FROM pbldst, pblokt " +
                                " WHERE(pbldst.npl = pblokt.npl) " +
                                "     and(pbldst.nplblk = pblokt.nplblk) " +
                                "     and((pbldst.clangjap = @clangjap) " +
                                "     and(pbldst.npl = @npl)) " +
                                " ORDER BY pbldst.xplblk ASC  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<Block>(strCommand, new { clangjap, npl }).ToList();
            }
            return list;
        }
        #endregion

        #region Запчасти
        // Выбор группы запчастей
        public static List<int> GetSelectionOfSparePartGroup(string npl)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT 1 FROM PLNRMT WHERE PLNRMT.NPL = @npl;  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { npl }).ToList();
            }
            return list;
        }
        //  Список крупноузловых групп
        public static List<LargeGroupSpareParts> GetListLargeGroupSpareParts(string clangjap, string npl, string hmodtyp)
        {
            List<LargeGroupSpareParts> list = new List<LargeGroupSpareParts>();

            string strCommand = " SELECT pgrout.clangjap,  " +
                           "       pgrout.nplgrp,  " +
                           "       pgrout.xplgrp,    " +
                           "       pgrout.nplgrpseq,    " +
                           "       pgrout.cengnfrm " +
                           "  FROM pgrout " +
                           " WHERE pgrout.clangjap = @clangjap " +
                           "   AND EXISTS(SELECT 1 " +
                           "                  FROM pblokt, pblmtt " +
                           "                 WHERE pblokt.nplgrp = pgrout.nplgrp " +
                           "                   AND pblokt.npl = pblmtt.npl " +
                           "                   AND pblokt.nplblk = pblmtt.nplblk " +
                           "                   AND pblokt.npl = @npl " +
                           $"                   AND pblmtt.hmodtyp IN( {hmodtyp} )); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<LargeGroupSpareParts>(strCommand, new { clangjap, npl }).ToList();
            }
            return list;
        }
        //  Уточнение типов авто
        public static List<Grade> GetListGrade(string hmodtyp)
        {
            List<Grade> list = new List<Grade>();

            string strCommand = " SELECT DISTINCT pmotyt.xgradefulnam,  " +
                        "          pmotyt.ctrsmtyp " +
                        "     FROM pmotyt " +
                        $"    WHERE pmotyt.hmodtyp in ( {hmodtyp} ) " +
                        " ORDER BY pmotyt.xgradefulnam ASC; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<Grade>(strCommand).ToList();
            }
            return list;
        }
        //   Отобрать блоки запчастей для выбранной крупноузловой группы "pblokt"."nplgrp"
        public static List<PartsBlocks> GetListPartsBlocks(string clangjap, string nplgrp, string npl, string hmodtyp)
        {
            List<PartsBlocks> list = new List<PartsBlocks>();

            string strCommand = " SELECT DISTINCT pblokt.nplblk, " +
                        "          pblokt.cmnopt1 AS VOPT1,   " +
                        "          pblokt.cmnopt2 AS VOPT2,   " +
                        "          pblokt.cmnopt3 AS VOPT3,   " +
                        "          pblokt.cmnopt4 AS VOPT4,   " +
                        "          pblokt.cmnopt5 AS VOPT5,   " +
                        "          pblokt.cmnopt1 AS NVOPT1,  " +
                        "          pblokt.cmnopt2 AS NVOPT2,  " +
                        "          pblokt.cmnopt3 AS NVOPT3,  " +
                        "          pblokt.cmnopt4 AS NVOPT4,   " +
                        "          pblokt.cmnopt5 AS NVOPT5,   " +
                        "          pblokt.nplblk_ref,    " +
                        "          pbldst.xplblk,    " +
                        "          pbldst.xplblkdiff,   " +
                        "          pblokt.cmnoptallw, " +
                        "          pblokt.nills, " +
                        "          0 AS HasNote, " +
                        "          pblokt.nplblkedit " +
                        "     FROM pblokt " +
                        "          JOIN pbldst " +
                        "             ON pblokt.npl = pbldst.npl " +
                        "             AND pblokt.nplblk = pbldst.nplblk " +
                        "             and pbldst.clangjap = @clangjap " +
                        "             and pbldst.npl = @npl " +
                        "    WHERE pblokt.nplgrp in (@nplgrp) and " +
                        "          (pblokt.nplblk in (SELECT DISTINCT pblmtt.nplblk " +
                        "                                      FROM pblmtt " +
                        "                                     WHERE(pblmtt.npl = @npl) AND " +
                        $"                                           (pblmtt.hmodtyp in ( {hmodtyp} ))) ) " +
                        " ORDER BY pblokt.nplblk; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<PartsBlocks>(strCommand, new { clangjap, nplgrp, npl }).ToList();
            }
            return list;
        }
        //  Выборка пользовательских примечаний
        public static List<notes> GetListNotes(int partslist_number)
        {
            List<notes> list = new List<notes>();
            //  не работает, таблицы notes Нет в базе???
            string strCommand = " SELECT  notes.note_id ,   notes.note_text ,     notes.block_number  " +
                                " FROM notes " +
                                " WHERE(notes.partslist_number = @partslist_number) " +
                                "       AND " +
                                "       (notes.block_number is not NULL); ";

            try
            {
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<notes>(strCommand, new { partslist_number }).ToList();
                }
            }
            catch(Exception ex)
            {
                list = null;
            }

            return list;
        }
        //   Перепроверка типов авто для выбранного блока запчастей
        public static List<int> GetTypeAvtoForBlockSpareParts(string npl, string nplblk, string hmodtyp)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT  pblmtt.hmodtyp  " +
                                " FROM pblmtt " +
                                " WHERE(pblmtt.npl = @npl) " +
                                "   and(pblmtt.nplblk = @nplblk) " +
                                $"     and(pblmtt.hmodtyp in ({ hmodtyp })); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { npl, nplblk }).ToList();
            }
            return list;
        }
        //  Список запчастей
        public static List<SpareParts> GetListSpareParts(string clangjap, string npl, string nplblk, string hmodtyp)
        {
            List<SpareParts> list = new List<SpareParts>();

            #region strCommand
            string strCommand = " SELECT DISTINCT pblpat.nplpartref,  " +
                        " ppartt.npartgenu,    " +
                        " ppartt.xpartext + ' ' + pbprmt.xpartrmrk AS xpartextrmrk,  " +
                        " pblpat.csernumcont,    " +
                        " ppartt.ccol,    " +
                        " ppartt.cedit,    " +
                        " pblpat.nplpartrefseq,   " +
                        " 0 AS HasNote, " +
                        " 0 As OnShoppingList, " +
                        " pblpat.xordergun, " +
                        " pblpat.hpartplblk, " +
                        " ppartt.creplblpart, " +
                        " ppartt.cmrkting, " +
                        " '0' AS showMark, " +
                        " ppasat.pretc1, " +
                        " ppasat.pretc2, " +
                        " ppasat.pretc3, " +
                        " ppasat.pretc4, " +
                        " ppasat.pretc5, " +
                        " ppasat.pretc6, " +
                        " ppasat.pretc7, " +
                        " ppasat.pretc8, " +
                        " ppasat.pretc9, " +
                        " ppasat.pretc10, " +
                        " ppasat.pretc11, " +
                        " ppasat.pretc12, " +
                        " ppasat.pretc13, " +
                        " ppasat.pretc14, " +
                        " ppasat.pretc15, " +
                        " ppasat.pretc16, " +
                        " ppasat.pretc17, " +
                        " ppasat.pretc18, " +
                        " ppasat.pretc19, " +
                        " ppasat.pretc20, " +
                        " ppasat.pretc21, " +
                        " ppasat.pretc22, " +
                        " ppasat.pretc23, " +
                        " ppasat.pretc24, " +
                        " ppasat.pretc25, " +
                        " ppasat.pretc26, " +
                        " ppasat.pretc27, " +
                        " ppasat.pretc28, " +
                        " ppasat.pretc29, " +
                        " ppasat.pretc30, " +
                        " 0 AS nser, " +
                        " 9.99 AS pretc_local " +
                        "  FROM pblpat " +
                        "  join ppartt on(ppartt.clangjap = @clangjap " +
                        "        and   ppartt.npartgenu = pblpat.npartgenu) " +
                        "  join ppasat on(ppartt.npartgenu = ppasat.npartgenu) " +
                        "  left outer join pbprmt on(pbprmt.hpartplblk = pblpat.hpartplblk " +
                        "        and  pbprmt.clangjap = @clangjap) " +
                        " WHERE pblpat.npl = @npl AND " +
                        "       pblpat.nplblk = @nplblk AND " +
                        "        ( " +
                        "         (EXISTS(SELECT pb.hmodtyp " +
                        "              FROM pbpmtt AS pb " +
                        "            WHERE pb.hpartplblk = pblpat.hpartplblk AND " +
                        $"                 pb.hmodtyp IN( { hmodtyp } ))) " +
                        " OR " +
                        "     ( " +
                        "      (NOT EXISTS(SELECT pb.hmodtyp " +
                        "    FROM pbpmtt AS pb " +
                        "   WHERE pb.hpartplblk = pblpat.hpartplblk)) " +
                        "   AND " +
                        " (EXISTS(SELECT pb.hmodtyp " +
                        "     FROM pblmtt AS pb " +
                        "  WHERE pb.npl = pblpat.npl AND " +
                        "  pb.nplblk = pblpat.nplblk AND " +
                        $"      pb.hmodtyp IN( { hmodtyp } ))))); "; 
            #endregion

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<SpareParts>(strCommand, new { clangjap, npl, nplblk }).ToList();
            }
            return list;
        }
        #endregion

        #region Цены
        //   Поиск номера по позиции на рисунке
        public static List<int> GetSearchForNumberByPositionInFigure(string npl, string illustrationnumber, int x, int y)
        {
            List<int> list = new List<int>();

            string strCommand = "  select hotspots.partreferencenumber " +
                                "   from hotspots " +
                                $" WHERE (hotspots.npl = '{npl}') " +
                                $" AND (hotspots.illustrationnumber = '{illustrationnumber}' ) " +
                                $" AND ({x} between hotspots.min_x and hotspots.max_x) " +
                                $" AND ({y} between hotspots.min_y and hotspots.max_y); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand).ToList();
            }
            return list;
        }
        //   Список типов авто выбранной запчасти
        //   не работает - нет таблицы vw_blockpartsmodeltypes
        public static List<int> GetTypeAvtoSelectedSpareParts(int clangjap, string npl, string nplblk, int nplpartref, string npartgenu)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT DISTINCT  vw_blockpartsmodeltypes.hmodtyp  " +
                                " FROM vw_blockpartsmodeltypes " +
                                " WHERE(vw_blockpartsmodeltypes.clangjap = @clangjap) " +
                                " and(vw_blockpartsmodeltypes.npl = @npl) " +
                                " and(vw_blockpartsmodeltypes.nplblk = @nplblk) " +
                                " and(vw_blockpartsmodeltypes.nplpartref = @nplpartref) " +
                                " and(vw_blockpartsmodeltypes.npartgenu = @npartgenu) " +
                                " ORDER BY vw_blockpartsmodeltypes.hmodtyp ASC;  ";

            try
            {
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<int>(strCommand, new { clangjap, npl, nplblk, nplpartref, npartgenu }).ToList();
                }
            }
            catch(Exception ex)
            {
                list = null;
            }

            return list;
        }
        //  После нажатия на Ок
        // Непонятно  ????
        // не работает - нет таблицы car_info
        public static List<car_info> GetCarInfo(string vin)
        {
            List<car_info> list = new List<car_info>();

            string strCommand = " SELECT  " +
                                "   car_info.vin ,   " +
                                " 	car_info.partslist_number ,    " +
                                " 	car_info.modelname ,    " +
                                " 	car_info.number_of_doors ,    " +
                                " 	car_info.modelyear ,   " +
                                " 	car_info.european_area_code ,  " +
                                " 	car_info.grade_full_name ,  " +
                                " 	car_info.transmission_type ,  " +
                                " 	car_info.exterior_hes_colour_type ,  " +
                                " 	car_info.interior_colour_type ,  " +
                                " 	car_info.frame_serial_number ,   " +
                                " 	car_info.engine_serial_number ,   " +
                                " 	car_info.carburator_serial_number ,  " +
                                " 	car_info.transmission_serial_number ,   " +
                                " 	car_info.main_option_codes ,    " +
                                " 	car_info.creator_name ,   " +
                                " 	car_info.creator_timestamp ,   " +
                                " 	car_info.cmodtypfrm " +
                                " FROM car_info " +
                                " where  car_info.vin = @vin;  ";


            try
            {
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<car_info>(strCommand, new { vin }).ToList();
                }
            }
            catch
            {
                list = null;
            }

            return list;
        }
        // Непонятно
        // не работает - нет таблицы  job_frt
        public static List<job_frt> GetJobFrt(string job_id)
        {
            List<job_frt> list = new List<job_frt>();

            string strCommand = " SELECT   " +
                                " job_frt.job_id ,  " +
                                " job_frt.partslist_number ,   " +
                                " job_frt.block_number ,  " +
                                " job_frt.language_code , " +
                                " job_frt.part_frt_number , " +
                                " job_frt.block_number_edited " +
                                " FROM job_frt " +
                                " where  job_frt.job_id = @job_id;  ";

            try
            {
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<job_frt>(strCommand, new { job_id }).ToList();
                }
            }
            catch
            {
                list = null;
            }
            return list;
        }
        //   Получить цену на выбранную запчасть
        public static List<decimal> GetTypeAvtoSelectedSpareParts2(string npartgenu)
        {
            List<decimal> list = new List<decimal>();

            string strCommand = " SELECT pretc13 FROM ppasat WHERE npartgenu = @npartgenu;  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<decimal>(strCommand, new { npartgenu }).ToList();
            }
            return list;
        }
        #endregion

        #region Запчасти-2
        //  Существую ли запчасти - количество
        public static int GetQtySpareParts(int clangjap, string npartgenu)
        {
            int qty = 0;

            string strCommand = " SELECT count(*)  " +
                                " FROM ppartt  " +
                                "     LEFT OUTER JOIN ppasat " +
                                "         ON(ppartt.npartgenu = ppasat.npartgenu) " +
                                " WHERE((ppartt.clangjap = @clangjap) " +
                                $" AND(ppartt.npartgenu like '{npartgenu}%' ));  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                qty = db.Query<int>(strCommand, new { clangjap }).FirstOrDefault();
            }
            return qty;
        }
        //  Найти запчасти
        public static List<SpareParts2> GetFindSpareParts(int clangjap, string npartgenu)
        {
            List<SpareParts2> list = new List<SpareParts2>();

            #region strCommand
            string strCommand = " SELECT ppartt.npartgenu, " +
                "          0  selected,    " +
                "          ppartt.cedit,    " +
                "          ppartt.xpartext,    " +
                "          ppasat.pretc8,  " +
                "          ppasat.pretc1,  " +
                "          ppasat.pretc2,  " +
                "          ppasat.pretc3,  " +
                "          ppasat.pretc4,  " +
                "          ppasat.pretc5,   " +
                "          ppasat.pretc6,  " +
                "          ppasat.pretc7,  " +
                "          ppasat.pretc9,  " +
                "          ppasat.pretc10,  " +
                "          ppasat.pretc11,  " +
                "          ppasat.pretc12,   " +
                "          ppasat.pretc13,   " +
                "          ppasat.pretc14,   " +
                "          ppasat.pretc15, " +
                "          ppasat.pretc16,    " +
                "          ppasat.pretc17,   " +
                "          ppasat.pretc18,   " +
                "          ppasat.pretc19,    " +
                "          ppasat.pretc20,   " +
                "          ppasat.pretc21,   " +
                "          ppasat.pretc22,   " +
                "          ppasat.pretc23,    " +
                "          ppasat.pretc24,   " +
                "          ppasat.pretc25,   " +
                "          ppasat.pretc26, " +
                "          ppasat.pretc27, " +
                "          ppasat.pretc28, " +
                "          ppasat.pretc29, " +
                "          ppasat.pretc30, " +
                "          0 AS unit_price_local_currency " +
                "     FROM ppartt " +
                "           LEFT OUTER JOIN ppasat " +
            "           ON(ppartt.npartgenu = ppasat.npartgenu) " +
            "    WHERE((ppartt.clangjap = @clangjap) AND " +
            $"          (ppartt.npartgenu like '{npartgenu}%' )) " +
            " ORDER BY ppartt.npartgenu ASC; ";
            #endregion

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<SpareParts2>(strCommand, new { clangjap }).ToList();
            }
            return list;
        }

        // Part Description
        public static int GetQtyPartDescription(int clangjap, string xpartext)
        {
            int qty = 0;

            string strCommand = " SELECT COUNT(*) " +
                                " FROM ppartt " +
                                "     LEFT OUTER JOIN ppasat " +
                                "         ON(ppartt.npartgenu = ppasat.npartgenu) " +
                                " WHERE((ppartt.clangjap = @clangjap) " +
                                $"     AND(ppartt.xpartext like '{xpartext}%' ));  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                qty = db.Query<int>(strCommand, new { clangjap }).FirstOrDefault();
            }
            return qty;
        }
        // Непонятно
        // получаем количество чего-то
        public static List<int> GetNser(int nsalgnp)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT nser FROM psagpt WHERE nsalgnp = @nsalgnp;  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { nsalgnp }).ToList();
            }
            return list;
        }
        #endregion

        #region VIN
        //  Список авто, к которым подходит выбранная запасть
        // Не работает, нет таблицы vw_blockpartsmodeltypes  - ????????
        public static List<Avto> GetAvtoList(string npartgenu, int clangjap)
        {
            List<Avto> list = new List<Avto>();

            #region strCommand
            string strCommand = " SELECT DISTINCT " +

                        "         pmotyt.cmodnamepc,   " +
                        "         pmotyt.dmodyr,    " +
                        "         pmotyt.xcardrs,    " +
                        "         pmotyt.carea,    " +
                        "         pmotyt.cmftrepc, " +
                        "         pmodlt.nphtmodbas,    " +
                        "         pmodlt.nphtmodmodif " +
                        "     FROM vw_blockpartsmodeltypes " +
                        "     JOIN pmotyt ON vw_blockpartsmodeltypes.hmodtyp = pmotyt.hmodtyp " +
                        "     JOIN pmodlt  ON pmotyt.cmodnamepc = pmodlt.cmodnamepc AND " +
                        "             pmotyt.dmodyr = pmodlt.dmodyr AND " +
                        "             pmotyt.xcardrs = pmodlt.xcardrs AND " +
                        "                         pmotyt.cmftrepc = pmodlt.cmftrepc " +
                        " 			,pmdldt " +
                        "   WHERE vw_blockpartsmodeltypes.npartgenu = @npartgenu " +
                        "     AND vw_blockpartsmodeltypes.clangjap = @clangjap " +
                        "         AND( " +
                        "             ( " +
                        "               NOT EXISTS(SELECT 1 " +
                        "                             FROM pmdldt " +
                        "                            WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                        "                              AND pmdldt.dmodyr = pmodlt.dmodyr " +
                        "                              AND pmdldt.xcardrs = pmodlt.xcardrs " +
                        "                              AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                        "                          ) " +
                        "               AND pmodlt.dmodlnch < now() " +
                        "             ) " +
                        "          OR( " +
                        "                   EXISTS(SELECT 1 " +
                        "                         FROM pmdldt " +
                        "                        WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                        "                          AND pmdldt.dmodyr = pmodlt.dmodyr " +
                        "                          AND pmdldt.xcardrs = pmodlt.xcardrs " +
                        "                          AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                        "                          AND pmdldt.dmodlnch < now() " +
                        "                      ) " +
                        "             ) " +
                        "             ) " +
                        "     ORDER BY pmotyt.cmodnamepc, pmotyt.dmodyr, pmotyt.xcardrs, pmotyt.carea;  ";
            #endregion

            try
            {
                using (IDbConnection db = new MySqlConnection(strConn))
                {
                    list = db.Query<Avto>(strCommand, new { npartgenu, clangjap }).ToList();
                }
            }
            catch
            {
                list = null;
            }

            return list;
        }
        //  Определить Информация про модель
        public static List<ModelInfo> GetModelInfoList(string cmodnamepc, int dmodyr, int xcardrs, string carea, string cmftrepc)
        {
            List<ModelInfo> list = new List<ModelInfo>();

            #region strCommand
            string strCommand = " SELECT DISTINCT " +
                                "     pmodlt.nphtmodbas,    " +
                                " 	pmodlt.nphtmodmodif,   " +
                                " 	pmotyt.npl, " +
                                " 	pmotyt.cmftrepc " +
                                " FROM pmodlt " +
                                "     JOIN pmotyt ON pmodlt.cmodnamepc = pmotyt.cmodnamepc " +
                                "         AND pmodlt.xcardrs = pmotyt.xcardrs " +
                                "         AND pmodlt.dmodyr = pmotyt.dmodyr " +
                                "         AND pmodlt.cmftrepc = pmotyt.cmftrepc " +
                                " WHERE(pmodlt.cmodnamepc = @cmodnamepc ) " +
                                "     AND(pmodlt.dmodyr = @dmodyr) " +
                                "         AND(pmodlt.xcardrs = @xcardrs) " +
                                "     AND(pmotyt.carea = @carea) " +
                                "         AND(pmodlt.cmftrepc = @cmftrepc) " +
                                "         AND( " +
                                "             ( " +
                                "               NOT EXISTS(SELECT 1 " +
                                "                             FROM pmdldt " +
                                "                            WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                                "                              AND pmdldt.dmodyr = pmodlt.dmodyr " +
                                "                              AND pmdldt.xcardrs = pmodlt.xcardrs " +
                                "                              AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                                "                          ) " +
                                "               AND pmodlt.dmodlnch < now() " +
                                "             ) " +
                                "          OR( " +
                                "                   EXISTS(SELECT 1 " +
                                "                         FROM pmdldt " +
                                "                        WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                                "                          AND pmdldt.dmodyr = pmodlt.dmodyr " +
                                "                          AND pmdldt.xcardrs = pmodlt.xcardrs " +
                                "                          AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                                "                          AND pmdldt.dmodlnch < now() " +
                                "                      ))); ";
            #endregion

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<ModelInfo>(strCommand, new { cmodnamepc, dmodyr, xcardrs, carea, cmftrepc }).ToList();
            }
            return list;
        }
        //  Список типов по выбранным параметрам
        public static List<int> GetListTypes(string cmodnamepc, int dmodyr, int xcardrs, string carea)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT DISTINCT " +
                                " pmotyt.hmodtyp " +
                                " FROM pmotyt " +
                                " WHERE(pmotyt.cmodnamepc = @cmodnamepc) " +
                                " and(pmotyt.dmodyr = @dmodyr) " +
                                " and(pmotyt.xcardrs = @xcardrs) " +
                                " and(pmotyt.carea = @carea);  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { cmodnamepc, dmodyr, xcardrs, carea }).ToList();
            }
            return list;
        }
        //Список типов по выбранным параметрам - 2
        public static List<int> GetListTypesByParam(string NPL)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT 1 FROM PLNRMT WHERE PLNRMT.NPL = @NPL;  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { NPL }).ToList();
            }
            return list;
        }
        //  Список блоков по выбранным параметрам авто Block3
        public static List<Block3> GetListBlock3ByParamAvto(string clangjap, string npl, string hmodtyp)
        {
            List<Block3> list = new List<Block3>();

            string strCommand = "  SELECT pgrout.clangjap, " +
                           "       pgrout.nplgrp,  " +
                           "       pgrout.xplgrp, " +
                           "       pgrout.nplgrpseq,  " +
                           "       pgrout.cengnfrm " +
                           "  FROM pgrout " +
                           "  WHERE pgrout.clangjap = '2' " +
                           "   AND EXISTS(SELECT 1 " +
                           "                  FROM pblokt, pblmtt " +
                           "                 WHERE pblokt.nplgrp = pgrout.nplgrp " +
                           "          AND pblokt.npl = pblmtt.npl " +
                           "                       AND pblokt.nplblk = pblmtt.nplblk " +
                           "                       AND pblokt.npl = @npl " +
                           $"          AND pblmtt.hmodtyp IN({hmodtyp}) ); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<Block3>(strCommand, new { clangjap, npl }).ToList();
            }
            return list;
        }
        //  Отобрать только те блоки в которых будет искомая запчасть для авто выбранного по параметрам
        public static List<Block4> GetListBlock4ByParamAvto(string npl, string npartgenu, string clangjap, string hmodtyp)
        {
            List<Block4> list = new List<Block4>();

            #region strCommand
            string strCommand = "  SELECT DISTINCT " +
                    "          pblokt.nplblk,    " +
                    "          pblokt.cmnopt1 AS VOPT1,  " +
                    "          pblokt.cmnopt2 AS VOPT2,  " +
                    "          pblokt.cmnopt3 AS VOPT3,  " +
                    "          pblokt.cmnopt4 AS VOPT4,  " +
                    "          pblokt.cmnopt5 AS VOPT5,  " +
                    "          pblokt.cmnopt1 AS NVOPT1, " +
                    "          pblokt.cmnopt2 AS NVOPT2,  " +
                    "          pblokt.cmnopt3 AS NVOPT3,   " +
                    "          pblokt.cmnopt4 AS NVOPT4,  " +
                    "          pblokt.cmnopt5 AS NVOPT5,  " +
                    "          pblokt.nplblk_ref,    " +
                    "          pbldst.xplblk,   " +
                    "          pbldst.xplblkdiff,  " +
                    "          pblokt.cmnoptallw, " +
                    "          pblokt.nills, " +
                    "          0 AS HasNote, " +
                    "          pblokt.nplblkedit " +
                    " FROM pblpat " +
                    "          left outer JOIN pblokt " +
                    "               ON(pblpat.npl = pblokt.npl " +
                    "           AND pblpat.nplblk = pblokt.nplblk) " +
                    "          left outer JOIN pbldst " +
                    "               ON(pblokt.npl = pbldst.npl " +
                    "           AND pblokt.nplblk = pbldst.nplblk) " +
                    " WHERE " +
                    "          pblpat.npl = @npl " +
                    $"      and pblpat.npartgenu like '{npartgenu}%' " +
                    "      AND pbldst.clangjap = @clangjap " +
                    "      and pbldst.npl = @npl " +
                    "      AND( " +
                    "         (EXISTS(SELECT pb.hmodtyp " +
                    "                 FROM pbpmtt AS pb " +
                    "               WHERE pb.hpartplblk = pblpat.hpartplblk AND " +
                    $"                      pb.hmodtyp IN({hmodtyp}))) " +
                    "          OR " +
                    "          ( " +
                    "         (NOT EXISTS(SELECT pb.hmodtyp " +
                    "                FROM pbpmtt AS pb " +
                    "               WHERE pb.hpartplblk = pblpat.hpartplblk)) AND " +
                    "                   (EXISTS(SELECT pb.hmodtyp " +
                    "                     FROM pblmtt AS pb " +
                    "                      WHERE pb.npl = pblpat.npl AND " +
                    "                         pb.nplblk = pblpat.nplblk AND " +
                    $"                         pb.hmodtyp IN({hmodtyp}))) " +
                    "                 ) " +
                    " 			)  " +
                    " ORDER BY pblokt.nplblk; ";
            #endregion

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<Block4>(strCommand, new { clangjap, npl }).ToList();
            }
            return list;
        }
        //  Проверка типов
        public static List<int> GetCheckTypes(string npl, string nplblk, string hmodtyp)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT  pblmtt.hmodtyp   " +
                                " FROM pblmtt " +
                                " WHERE(pblmtt.npl = @npl) " +
                                " and(pblmtt.nplblk = @nplblk) " +
                                $" and(pblmtt.hmodtyp in ({hmodtyp}));  ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { npl, nplblk}).ToList();
            }
            return list;
        }
        // Список запчастей
        public static List<SpareParts> GetListSpareParts2(string clangjap, string npl, string nplblk, string hmodtyp)
        {
            List<SpareParts> list = new List<SpareParts>();

            #region strCommand
            string strCommand = "    SELECT DISTINCT " +
                                "    pblpat.nplpartref,  " +
                                "    ppartt.npartgenu,   " +
                                "    ppartt.xpartext + ' ' + pbprmt.xpartrmrk AS xpartextrmrk, " +
                                "    pblpat.csernumcont,    " +
                                "    ppartt.ccol,   " +
                                "    ppartt.cedit,  " +
                                "    pblpat.nplpartrefseq,  " +
                                "    0 AS HasNote, " +
                                "    0 As OnShoppingList, " +
                                "    pblpat.xordergun, " +
                                "    pblpat.hpartplblk, " +
                                "    ppartt.creplblpart, " +
                                " 	ppartt.cmrkting, " +
                                " 	'0' AS showMark, " +
                                "   ppasat.pretc1, " +
                                " 	ppasat.pretc2, " +
                                " 	ppasat.pretc3, " +
                                " 	ppasat.pretc4, " +
                                " 	ppasat.pretc5, " +
                                " 	ppasat.pretc6, " +
                                " 	ppasat.pretc7, " +
                                " 	ppasat.pretc8, " +
                                " 	ppasat.pretc9, " +
                                " 	ppasat.pretc10, " +
                                " 	ppasat.pretc11, " +
                                " 	ppasat.pretc12, " +
                                " 	ppasat.pretc13, " +
                                " 	ppasat.pretc14, " +
                                " 	ppasat.pretc15, " +
                                " 	ppasat.pretc16, " +
                                " 	ppasat.pretc17, " +
                                " 	ppasat.pretc18, " +
                                " 	ppasat.pretc19, " +
                                " 	ppasat.pretc20, " +
                                " 	ppasat.pretc21, " +
                                " 	ppasat.pretc22, " +
                                " 	ppasat.pretc23, " +
                                " 	ppasat.pretc24, " +
                                " 	ppasat.pretc25, " +
                                " 	ppasat.pretc26, " +
                                " 	ppasat.pretc27, " +
                                " 	ppasat.pretc28, " +
                                " 	ppasat.pretc29, " +
                                " 	ppasat.pretc30, " +
                                " 	0 AS nser, " +
                                "     9.99 AS pretc_local " +
                                " FROM pblpat " +
                                "     join ppartt on(ppartt.clangjap = @clangjap " +
                                "         and ppartt.npartgenu = pblpat.npartgenu) " +
                                "     join ppasat on(ppartt.npartgenu = ppasat.npartgenu) " +
                                "         left outer join pbprmt on(pbprmt.hpartplblk = pblpat.hpartplblk " +
                                "         and pbprmt.clangjap = @clangjap) " +
                                " WHERE pblpat.npl = @npl AND " +
                                "          pblpat.nplblk = @nplblk AND " +
                                "         (   " +
                                "             (EXISTS(SELECT pb.hmodtyp " +
                                "                     FROM pbpmtt AS pb " +
                                "                   WHERE pb.hpartplblk = pblpat.hpartplblk AND " +
                                "                          pb.hmodtyp IN(@hmodtyp))) OR " +
                                "                 ( " +
                                "         (NOT EXISTS(SELECT pb.hmodtyp " +
                                "                FROM pbpmtt AS pb " +
                                "             WHERE pb.hpartplblk = pblpat.hpartplblk)) " +
                                "             AND " +
                                "             (EXISTS(SELECT pb.hmodtyp " +
                                "                 FROM pblmtt AS pb " +
                                "                 WHERE pb.npl = pblpat.npl AND " +
                                "                      pb.nplblk = pblpat.nplblk AND " +
                                "                      pb.hmodtyp IN(@hmodtyp))))); ";
            #endregion

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<SpareParts>(strCommand, new { clangjap, npl, nplblk, hmodtyp }).ToList();
            }
            return list;
        }
        //  VIN разборка
        public static List<int> GetTypesByVin(string nfrmpf, string nfrmseqepcstrt, string nfrmseqepcend)
        {
            List<int> list = new List<int>();

            string strCommand = " SELECT pmotyt.hmodtyp " +
                                " FROM pmodlt " +
                                "     JOIN pmotyt " +
                                "         ON(pmodlt.cmodnamepc = pmotyt.cmodnamepc " +
                                "         AND pmodlt.dmodyr = pmotyt.dmodyr " +
                                "         AND pmodlt.xcardrs = pmotyt.xcardrs) ,  " +
                                " 	  pmdldt " +
                                "    WHERE(pmotyt.nfrmpf = @nfrmpf) " +
                                "     AND(pmotyt.nfrmseqepcstrt <= @nfrmseqepcstrt) " +
                                "     AND(pmotyt.nfrmseqepcend >= @nfrmseqepcend) " +
                                "     AND((NOT EXISTS(SELECT 1 " +
                                "             FROM pmdldt " +
                                "             WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                                "             AND pmdldt.dmodyr = pmodlt.dmodyr " +
                                "             AND pmdldt.xcardrs = pmodlt.xcardrs " +
                                "             AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                                "             ) " +
                                "         AND pmodlt.dmodlnch < now()) " +
                                "         OR(EXISTS(SELECT 1 " +
                                "             FROM pmdldt " +
                                "             WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                                "             AND pmdldt.dmodyr = pmodlt.dmodyr " +
                                "             AND pmdldt.xcardrs = pmodlt.xcardrs " +
                                "             AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                                "             AND pmdldt.dmodlnch < now())) ); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<int>(strCommand, new { nfrmpf, nfrmseqepcstrt, nfrmseqepcend }).ToList();
            }
            return list;
        }
        //Получение списка Area, список для VIN - SHHCG7540YU003985
        public static List<string> GetAreaList(string hmodtyp, string ngnp)
        {
            List<string> list = new List<string>();

            string strCommand = "SELECT DISTINCT pmotyt.carea " +
                "        FROM pmotyt LEFT OUTER JOIN pmodlt ON(pmotyt.cmodnamepc = pmodlt.cmodnamepc AND " +
                "                                                    pmotyt.dmodyr = pmodlt.dmodyr AND " +
                "                                                    pmotyt.xcardrs = pmodlt.xcardrs) " +
                "             ,pmdldt " +
                $"       WHERE pmotyt.hmodtyp IN ({hmodtyp}) " +
                "         AND( " +
                "             ( " +
                "               NOT EXISTS(SELECT 1 " +
                "                             FROM pmdldt " +
                "                            WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                "                              AND pmdldt.dmodyr = pmodlt.dmodyr " +
                "                              AND pmdldt.xcardrs = pmodlt.xcardrs " +
                "                              AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                "                              AND pmdldt.ngnp = @ngnp " +
                "                          ) " +
                "               AND pmodlt.dmodlnch < now() " +
                "             ) " +
                "          OR( " +
                "                   EXISTS(SELECT 1 " +
                "                         FROM pmdldt " +
                "                        WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                "                          AND pmdldt.dmodyr = pmodlt.dmodyr " +
                "                          AND pmdldt.xcardrs = pmodlt.xcardrs " +
                "                          AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                "                          AND pmdldt.ngnp = @ngnp " +
                "                          AND pmdldt.dmodlnch < now()))) " +
                " ORDER BY pmotyt.carea ASC; ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<string>(strCommand, new { ngnp }).ToList();
            }

            return list;
        }
        //  Информация о типе авто
        public static List<CarTypeInfo> GetListCarTypeInfo(string nfrmpf, string nfrmseqepcstrt, string nfrmseqepcend, string carea)
        {
            List<CarTypeInfo> list = new List<CarTypeInfo>();

            string strCommand = "   SELECT DISTINCT " +
                                "   pmotyt.hmodtyp, " +
                                " 	pmotyt.cmodnamepc, " +
                                " 	pmotyt.xcardrs, " +
                                " 	pmotyt.dmodyr, " +
                                " 	pmotyt.xgradefulnam, " +
                                " 	pmotyt.ctrsmtyp, " +
                                " 	pmotyt.cmftrepc " +
                                " FROM pmodlt " +
                                "     JOIN pmotyt " +
                                "         ON(pmodlt.cmodnamepc = pmotyt.cmodnamepc " +
                                "         AND pmodlt.dmodyr = pmotyt.dmodyr " +
                                "         AND pmodlt.xcardrs = pmotyt.xcardrs) " +
                                " 	,pmdldt " +
                                " WHERE(pmotyt.nfrmpf = @nfrmpf) " +
                                "     AND(pmotyt.nfrmseqepcstrt <= @nfrmseqepcstrt) " +
                                "     AND(pmotyt.nfrmseqepcend >= @nfrmseqepcend) " +
                                "         AND(pmotyt.carea <= @carea) " +
                                "         AND(pmotyt.carea >= @carea) " +
                                "         AND( " +
                                "             ( " +
                                "               NOT EXISTS(SELECT 1 " +
                                "                             FROM pmdldt " +
                                "                            WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                                "                              AND pmdldt.dmodyr = pmodlt.dmodyr " +
                                "                              AND pmdldt.xcardrs = pmodlt.xcardrs " +
                                "                              AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                                "                          ) " +
                                "               AND pmodlt.dmodlnch < now() " +
                                "             ) " +
                                "          OR( " +
                                "                   EXISTS(SELECT 1 " +
                                "                         FROM pmdldt " +
                                "                        WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                                "                          AND pmdldt.dmodyr = pmodlt.dmodyr " +
                                "                          AND pmdldt.xcardrs = pmodlt.xcardrs " +
                                "                          AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                                "                          AND pmdldt.dmodlnch < now()))); ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<CarTypeInfo>(strCommand, new { nfrmpf, nfrmseqepcstrt, nfrmseqepcend, carea }).ToList();
            }

            return list;
        }
        //   Информация про модель
        public static List<CarModel> GetCarModelInfo2(string cmodnamepc, int dmodyr, int xcardrs, string carea, string cmftrepc)
        {
            List<CarModel> list = new List<CarModel>();

            string strCommand = " SELECT DISTINCT " +
                            "     pmodlt.nphtmodbas,   " +
                            " 	pmodlt.nphtmodmodif,  " +
                            " 	pmotyt.npl, " +
                            " 	pmotyt.cmftrepc " +
                            " FROM pmodlt " +
                            "     JOIN pmotyt " +
                            "         ON pmodlt.cmodnamepc = pmotyt.cmodnamepc " +
                            "             AND pmodlt.xcardrs = pmotyt.xcardrs " +
                            "             AND pmodlt.dmodyr = pmotyt.dmodyr " +
                            "             AND pmodlt.cmftrepc = pmotyt.cmftrepc " +
                            " WHERE(pmodlt.cmodnamepc = @cmodnamepc) " +
                            "     AND(pmodlt.dmodyr = @dmodyr) " +
                            "     AND(pmodlt.xcardrs = @xcardrs) " +
                            "     AND(pmotyt.carea = @carea) " +
                            "         AND(pmodlt.cmftrepc = @cmftrepc) " +
                            "         AND( " +
                            "             ( " +
                            "               NOT EXISTS(SELECT 1 " +
                            "                             FROM pmdldt " +
                            "                            WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                            "                              AND pmdldt.dmodyr = pmodlt.dmodyr " +
                            "                              AND pmdldt.xcardrs = pmodlt.xcardrs " +
                            "                              AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                            "                          ) " +
                            "               AND pmodlt.dmodlnch < now() " +
                            "             ) " +
                            "          OR( " +
                            "           EXISTS(SELECT 1 " +
                            "                         FROM pmdldt " +
                            "                        WHERE pmdldt.cmodnamepc = pmodlt.cmodnamepc " +
                            "                          AND pmdldt.dmodyr = pmodlt.dmodyr " +
                            "                          AND pmdldt.xcardrs = pmodlt.xcardrs " +
                            "                          AND pmdldt.cmftrepc = pmodlt.cmftrepc " +
                            "                          AND pmdldt.dmodlnch < now()))) ";

            using (IDbConnection db = new MySqlConnection(strConn))
            {
                list = db.Query<CarModel>(strCommand, new { cmodnamepc, dmodyr, xcardrs, carea, cmftrepc }).ToList();
            }
            return list;
        } 
        #endregion

    }
}
