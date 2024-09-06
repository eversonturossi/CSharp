using System.Diagnostics;

// Exemplo 01 ** matar processo específico
var processNameToKill = "nomedoprocesso";
var processList01 = Process.GetProcessesByName(processNameToKill);
if (processList01.Length <= 0)
    Console.WriteLine("No matching processes found to kill.");

foreach (Process process in processList01)
{
    try
    {
        Console.WriteLine($"Detalhes do processo:");
        Console.WriteLine($"Nome do processo: {process.ProcessName}");
        Console.WriteLine($"ID do processo: {process.Id}");
        Console.WriteLine($"Tempo de execução: {process.TotalProcessorTime}");
        Console.WriteLine($"Tempo de início: {process.StartTime}");
        Console.WriteLine($"Caminho completo do executável: {process.MainModule?.FileName}");

        process.Kill();
        Console.WriteLine($"Killed process with ID {process.Id} and name {process.ProcessName}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to kill process: {ex.Message}");
    }
}

// Exemplo 02 ** matar processo por palavra-chave
var processList02 = Process.GetProcesses();
var keyword = "partedonomedoprocesso";
foreach (Process process in processList02)
{
    var processName = process.ProcessName.ToLower();
    var processPath = process.StartInfo.FileName.ToLower();
    // Verificar se o nome do processo, ou o caminho do executável contém a palavra-chave
    if (processName.Contains(keyword) || processPath.Contains(keyword))
    {
        try
        {
            process.Kill();
            Console.WriteLine($"Processo {process.ProcessName} foi encerrado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao encerrar o processo {process.ProcessName}: {ex.Message}");
        }
    }
}
