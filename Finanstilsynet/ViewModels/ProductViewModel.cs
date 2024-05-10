namespace ViewModels
{
    public class ProductViewModel
    {
        public int ModelID { get; set; }
        public string MakerID { get; set; }
        public string Product_Type { get; set; }
        public int? Laptop_Speed { get; set; }
        public int? Laptop_RAM { get; set; }
        public int? Laptop_HardDisk { get; set; }
        public decimal? Laptop_Screen { get; set; }
        public int? PC_Speed { get; set; }
        public int? PC_RAM { get; set; }
        public int? PC_HardDisk { get; set; }
        public string PC_ReadDrive { get; set; }
        public string Printer_Color { get; set; }
        public string Printer_Type { get; set; }
        public int? Product_Price { get; set; }
        public string Maker_Color { get; set; }
    }
}