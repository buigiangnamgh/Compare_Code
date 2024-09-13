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
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.UC.ExamTreatmentFinish.ADO
{
    public class RoomExamADO
    {
        public bool IsCheck { get; set; }
        public long ID { get; set; }
        public string ROOM_CODE { get; set; }
        public string ROOM_NAME { get; set; }
        public long EXECUTE_ROOM_ID { get; set; }
        public long? MAX_APPOINTMENT_BY_DAY { get; set; }
        public short? IS_BLOCK_NUM_ORDER { get; set; }

        public RoomExamADO() { }

        public RoomExamADO(HIS_EXECUTE_ROOM data)
        {
            try
            {
                if (data != null)
                {
                    this.EXECUTE_ROOM_ID = data.ID;
                    this.ID = data.ROOM_ID;
                    this.ROOM_CODE = data.EXECUTE_ROOM_CODE;
                    this.ROOM_NAME = data.EXECUTE_ROOM_NAME;
                    this.MAX_APPOINTMENT_BY_DAY = data.MAX_APPOINTMENT_BY_DAY;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
