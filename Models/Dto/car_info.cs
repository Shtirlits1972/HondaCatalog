using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaCatalog.Models.Dto
{
    public class car_info
    {
		public string vin { get; set; }
		public string partslist_number { get; set; }
		public string modelname { get; set; }
		public string number_of_doors { get; set; }
		public string modelyear { get; set; }
		public string european_area_code { get; set; }
		public string grade_full_name { get; set; }
		public string transmission_type { get; set; }
		public string exterior_hes_colour_type { get; set; }
		public string interior_colour_type { get; set; }
		public string frame_serial_number { get; set; }
		public string engine_serial_number { get; set; }
		public string carburator_serial_number { get; set; }
		public string transmission_serial_number { get; set; }
		public string main_option_codes { get; set; }
		public string creator_name { get; set; }
		public string creator_timestamp { get; set; }
		public string cmodtypfrm { get; set; }
	}
}
