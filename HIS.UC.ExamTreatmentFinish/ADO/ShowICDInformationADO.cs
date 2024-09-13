/* IVT
 * @Project : hisnguonmo
 * Copyright (C) 2017 INVENTEC
 *  
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *  
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.
 *  
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.UC.ExamTreatmentFinish.ADO
{
    public class ShowICDInformationADO
    {
        public string SHOW_ICD_CODE { get; set; }
        public string SHOW_ICD_NAME { get; set; }
        public string SHOW_ICD_SUB_CODE { get; set; }
        public string SHOW_ICD_TEXT { get; set; }

        public ShowICDInformationADO() 
        { 
        }

        public ShowICDInformationADO(HIS_TREATMENT treatment)
        {
            if (treatment != null)
            {
                this.SHOW_ICD_CODE = treatment.SHOW_ICD_CODE;
                this.SHOW_ICD_NAME = treatment.SHOW_ICD_NAME;
                this.SHOW_ICD_SUB_CODE = treatment.SHOW_ICD_SUB_CODE;
                this.SHOW_ICD_TEXT = treatment.SHOW_ICD_TEXT;
            }
        }
    }
}
