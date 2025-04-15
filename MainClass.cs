using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modul8_103022300100
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            BankTransferConfig bankConfig = new BankTransferConfig();
            Config data = bankConfig.Read();
            Console.WriteLine("Nilai awal JSON");
            Console.WriteLine($"Bahasa: {data.lang}");
            Console.WriteLine($"Threshold: {data.transfer.threshold}");
            //Console.WriteLine($"Low Fee: {bankConfig.transfer.low_fee}");
            //Console.WriteLine($"High Fee: {bankConfig.transfer.high_fee}");

            Console.WriteLine("\nMasukkan jumlah transfer: ");
            int amount = int.Parse(Console.ReadLine());
            bankConfig.transferProcessor(amount);
            bankConfig.transferMethodMessage();
            bankConfig.transferConfirmationMsg();
            string status = Console.ReadLine();
            bankConfig.transferConfirmation(status);

        }
    }
}
