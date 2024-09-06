// Referências
// https://marcdias.com.br/c-controle-suas-tasks-usando-o-semaphoreslim-dica/
// https://gist.github.com/mdcarmo/2e9cc160ad4310c099ccc8665e93b4db

using System.Diagnostics;

var timeExecution = new Stopwatch();
Console.WriteLine($"Iniciando a execução Sem Semaforo");
timeExecution.Start();
InitProcessWithOutSemaphore();
timeExecution.Stop();
Console.WriteLine($"Execução finalizada Sem Semaforo: {timeExecution.Elapsed:hh\\:mm\\:ss\\.fff}");

var timeExecutionSemaphore = new Stopwatch();
Console.WriteLine($"Iniciando a execução Com Semaforo");
timeExecutionSemaphore.Start();
InitProcessWithSemaphore();
timeExecutionSemaphore.Stop();
Console.WriteLine($"Execução finalizada Com Semaforo: {timeExecutionSemaphore.Elapsed:hh\\:mm\\:ss\\.fff}");

public partial class Program
{
    private const int MAX = 30;
    static void InitProcessWithOutSemaphore()
    {
        var listTasks = new List<Task>();
        for (int i = 0; i < MAX; i++)
        {
            listTasks.Add(ProcessSemaphoreLess(i));
        }
        Task.WaitAll(listTasks.ToArray());
    }

    static void InitProcessWithSemaphore()
    {
        var semaphore = new SemaphoreSlim(1);
        var listTasks = new List<Task>();
        for (int i = 0; i < MAX; i++)
        {
            listTasks.Add(ProcessWithSemaphore(semaphore, i));
        }
        Task.WaitAll(listTasks.ToArray());
    }

    static async Task ProcessWithSemaphore(SemaphoreSlim semaphoreSlim, int id)
    {
        await semaphoreSlim.WaitAsync();
        Console.WriteLine($"Simulando a chamada a uma API qualquer com semaphore {id}");
        var httpClient = new HttpClient();
        await httpClient.GetAsync("http://httpstat.us/200?sleep=1000");
        semaphoreSlim.Release();
    }

    static async Task ProcessSemaphoreLess(int id)
    {
        Console.WriteLine($"Simulando a chamada a uma API qualquer sem semaphore {id}");
        var httpClient = new HttpClient();
        await httpClient.GetAsync("http://httpstat.us/200?sleep=1000");
    }
}