using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Management;

using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Drawing.Printing;

Console.WriteLine("--------------------- Impressoras (via System.Drawing.Printing.PrinterSettings.InstalledPrinters )---------------------------");
foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
{
    Console.WriteLine(printer);
}

Console.WriteLine("---------------------- Impressoras (via WMI 01) --------------------------");

var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
foreach (var printer in printerQuery.Get())
{
    var name = printer.GetPropertyValue("Name");
    var status = printer.GetPropertyValue("Status");
    var isDefault = printer.GetPropertyValue("Default");
    var isNetworkPrinter = printer.GetPropertyValue("Network");
    var isLocalPrinter = printer.GetPropertyValue("Local");
    var isShared = printer.GetPropertyValue("Shared");
    var isHidden = printer.GetPropertyValue("Hidden");
    var isDirect = printer.GetPropertyValue("Direct");
    var isPublished = printer.GetPropertyValue("Published");

    Console.WriteLine("{0} (Status: {1}, Default: {2}, Network: {3}", name, status, isDefault, isNetworkPrinter);
    Console.WriteLine("Local: {0}, Shared: {1}, Hidden: {2}, Direct: {3}, Published: {4}", isLocalPrinter, isShared, isHidden, isDirect, isPublished);
}

Console.WriteLine("---------------------- Impressoras (via WMI 02) --------------------------");

ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
objScope.Connect();
SelectQuery selectQuery = new SelectQuery();
selectQuery.QueryString = "Select * from win32_Printer";
ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
ManagementObjectCollection MOC = MOS.Get();
foreach (ManagementObject mo in MOC)
{
    Console.WriteLine(mo["Name"].ToString());
}

Console.WriteLine("------------------- Portas Seriais -----------------------------");

string[] ports = SerialPort.GetPortNames();
Console.WriteLine("The following serial ports were found:");
foreach (string port in ports)
{
    Console.WriteLine(port);
}

Console.WriteLine("--------------------- Dispositivos Instalados ---------------------------");

ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
foreach (ManagementObject queryObj in searcher.Get())
{
    Console.WriteLine("DeviceID: {0}", queryObj["DeviceID"]);
    Console.WriteLine("PNPDeviceID: {0}", queryObj["PNPDeviceID"]);
    Console.WriteLine("Name: {0}", queryObj["Name"]);
    Console.WriteLine("Description: {0}", queryObj["Description"]);
    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++");
}

public partial class Program
{

}
