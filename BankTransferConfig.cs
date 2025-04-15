using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace modul8_103022300100
{
    class Transfer
    {
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }
    }
    class Confirmation
    {
        public string en { get; set; }
        public string id { get; set; }
    }
    class Config
    {
        public string lang { get; set; }
        public Transfer transfer { get; set; }
        public List<string> methods { get; set; }
        public Confirmation confirmation { get; set; }
    }
    class BankTransferConfig
    {
        public Config config;
        public const string filePath = "bank_transfer_config.json";
        public BankTransferConfig()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            try
            {
                string configJson = File.ReadAllText(filePath);
                config = JsonSerializer.Deserialize<Config>(configJson);
            }
            catch (FileNotFoundException)
            {
                config = new Config();
                saveConfig();
            }
        }
        public void saveConfig()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string configJson = JsonSerializer.Serialize(config, options);
            File.WriteAllText(filePath, configJson);
        }

        public void transferProcessor(int amount)
        {
            int totalAmount = 0;
            int transferFee = 0;
            if (amount <= config.transfer.threshold)
            {
                transferFee = config.transfer.low_fee;
            }
            else if (amount > config.transfer.high_fee)
            {
                transferFee = config.transfer.high_fee;
            }
            totalAmount = amount + transferFee;

            if (config.lang == "en")
            {
                System.Console.WriteLine($"Transfer fee = {transferFee}");
                System.Console.WriteLine($"Total Amount = {totalAmount}");
            }
            else if (config.lang == "id")
            {
                System.Console.WriteLine($"Biaya Transfer = {transferFee}");
                System.Console.WriteLine($"Total Biaya = {totalAmount}");
            }
        }
        public void transferMethodMessage()
        {
            if (config.lang == "en")
            {
                Console.WriteLine("Select transfer method: ");
            }
            else if (config.lang == "id")
            {
                Console.WriteLine("Pilih metode transfer: ");
            }

            for(int i = 0; i < config.methods.Count; i++)
            {
                Console.WriteLine((i+1) + config.methods[i]);
            }
        }
        public void transferConfirmationMsg()
        {
            if (config.lang == "en")
            {
                Console.WriteLine($"Please type {config.confirmation.en}");
            }
            else if (config.lang == "id")
            {
                Console.WriteLine($"Ketik {config.confirmation.id}");
            }
        }
        public void transferConfirmation(string confirmation)
        {
            if (config.lang == "en")
            {
                if (confirmation.ToLower() == "yes")
                {
                    Console.WriteLine("The transfer is completed");
                }
                else if (confirmation.ToLower() == "no")
                {
                    {
                        Console.WriteLine("Transfer is cancelled");
                    }
                }
                else if (config.lang == "id")
                {
                    if (confirmation.ToLower() == "ya")
                    {
                        Console.WriteLine("Proses transfer berhasil");
                    }
                    else if (confirmation.ToLower() == "tidak")
                    {
                        Console.WriteLine("Transfer dibatalkan");
                    }
                }
            }
        }
    }
}
