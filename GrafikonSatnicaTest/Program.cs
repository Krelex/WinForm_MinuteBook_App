﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grafikon.Model;

namespace GrafikonSatnicaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create working object
            Satnica ob1 = new Satnica();

            //Open xls. temp and initialize it to HSSFWorkbook
            HSSFWorkbook workbook = Satnica.openTemp();

            //Point to sheet of workbook
            ISheet sheet = workbook.GetSheetAt(0);

            //Dohvacanje reda i kolone exemple
            //sheet.GetRow(11).Cells[0].SetCellValue("8");


            //Hardcode data
            //ob1.godina= 2018;
            //ob1.mjesec = 4;
            //ob1.startWork = 9;
            //ob1.endWork = 17;
            //ob1.ime = "Filip";
            //ob1.prezime = "Čogelja";


            //Dynamic data
            Console.WriteLine("Upisite IME zaposlenika");
            ob1.ime = Console.ReadLine();
            Console.WriteLine("Upisite PREZIME zaposlenika");
            ob1.prezime = Console.ReadLine();
            Console.WriteLine("Upisite GODINU format['yyyy']");
            ob1.godina = int.Parse(Console.ReadLine());
            Console.WriteLine("Upisite MJESEC format['MM']");
            ob1.mjesec = int.Parse(Console.ReadLine());
            Console.WriteLine("Upisite POCETAK RADA zaposlenika format['hh']");
            ob1.startWork = int.Parse(Console.ReadLine());
            Console.WriteLine("Upisite KRAJ RADA zaposlenika format['hh']");
            ob1.endWork = int.Parse(Console.ReadLine());


            //Set name and surname
            sheet.GetRow(6).Cells[1].SetCellValue(ob1.ime + " " + ob1.prezime);

            //Set starting date
            sheet.GetRow(8).Cells[1].SetCellValue(ob1.FirstDay());


            //Set ending date 
            sheet.GetRow(8).Cells[4].SetCellValue(ob1.LastDay());

            //Preset for populating data
            int startingRow = 11;
            DateTime datum = ob1.FirstDay();
            int endingRow = ob1.DaysInMonth() + startingRow;

            for (int i = startingRow; i < endingRow; i++)
            {

                sheet.GetRow(i).Cells[0].SetCellValue(datum.Date);
                sheet.GetRow(i).Cells[1].SetCellValue(datum.Date.ToString("ddd"));

                if (datum.DayOfWeek != DayOfWeek.Saturday && datum.DayOfWeek != DayOfWeek.Sunday && !(ob1.holidayCheck(datum))) 
                {
                    sheet.GetRow(i).Cells[2].SetCellValue(ob1.startWork);
                    sheet.GetRow(i).Cells[3].SetCellValue(ob1.endWork);
                    sheet.GetRow(i).Cells[5].SetCellValue(ob1.TotalWork());
                }
                datum = datum.AddDays(1);
            }

            Satnica.saveTemp(workbook);
        }


    }
}