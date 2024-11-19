using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.Utiles
{
    public static class FechaHelper
    {

        public static int DevolverNroDiaPeru(this DateTime fecha)
        {
            int diaSemana = 0;
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    diaSemana = 5;
                    break;
                case DayOfWeek.Monday:
                    diaSemana = 1;
                    break;
                case DayOfWeek.Saturday:
                    diaSemana = 6;
                    break;
                case DayOfWeek.Sunday:
                    diaSemana = 7;
                    break;
                case DayOfWeek.Thursday:
                    diaSemana = 4;
                    break;
                case DayOfWeek.Tuesday:
                    diaSemana = 2;
                    break;
                case DayOfWeek.Wednesday:
                    diaSemana = 3;
                    break;
                default:
                    break;
            }

            return diaSemana;

        }

    }
}
