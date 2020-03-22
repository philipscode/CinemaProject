# Web-Api для кинотеатра

ER-диаграмма: https://drive.google.com/open?id=10Etx68r2WGsJxetsUBKCCTvKIfNORvsn

**Стек технологий:**

- .NET Core 3.0
- PostgreSQL

**Подключенные библиотеки:**

- AutoMapper
- EntityFrameworkCore
- Serilog
- Swagger

# Теоретическая часть

## Задание №1

#### Объясни смысл следующего кода:

```c#
public sealed class Cinema
{
    private static volatile Cinema _cinema;
    private static readonly object SyncRoot = new object();

    private Cinema() { }

    public static Cinema GetInstance()
    {
        if (_cinema == null)
        {
            lock(SyncRoot)
            {
                if (_cinema == null)
                {
                    _cinema = new Cinema();
                }
            }
        }
        return _cinema;
    }
}
```

В данном примере реализован паттерн синглтон. Приватный конструктор
предотвращает возможность инициализации объекта класса извне, а метод GetInstance
всегда будет возвращать один и тот же экземпляр класса Cinema. Кроме того, переменная
_cinema объявлена с ключевым словом volatile, что сигнализирует компилятору, что
ее значение может изменяться в нескольких потоках. И действительно, в методе GetInstance
используется механизм lock, гарантирующий доступ к последующему блоку кода ровно одним
потоком. Это предотвращает возможность нескольким потокам одновременно проинициализировать
переменную _cinema, что нарушило бы паттерн синглтон. Таким образом, гарантируется
работа с единственным экземпляром класса Cinema даже в случае многопоточности.

## Задание №2

#### Как можно оптимизировать следующий код:

```c#
var movies = Enumerable.Range(0, 10000)
	.Select(i => new Movie {Id = i, Title = $"Movie{i}"})
	.ToList();
var sessions = Enumerable.Range(1, 100000)
	.Select(i => new Session {Id = i, MovieId = i / 10})
	.ToList();
foreach (var session in sessions)
{
	session.MovieTitle = movies
        .FirstOrDefault(movie => movie.Id == session.MovieId)?.Title;
}

class Session
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    Public string MovieTitle { get; set; }
}
class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
}

```

Запросы к источникам данных можно распараллелить с помощью метода AsParallel. 
Исходные численные промежутки, если это возможно, будут разбиваться на части, над
каждой из которых будет отдельно производиться операция. Кроме того, можно избавиться от
последнего цикла foreach, внутреннюю операцию внутри которого можно поместить внутрь
второго select.

```c#
var movies=  Enumerable.Range(0, 10000)
    .AsParallel()
    .Select(i => new Movie {Id = i, Title = $"Movie{i}"})
    .ToList();

var sessions = Enumerable.Range(1, 100000)
    .AsParallel()
    .Select(i => new Session
    {
        Id = i,
        MovieId = i / 10,
        MovieTitle = movies.FirstOrDefault(movie => movie.Id == i / 10)?.Title
    })
    .ToList();
    
class Session
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
}
class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
}
```

## Задание №3

#### Проанализируй реализацию и предложи рефакторинг, позволяющий расширять форматы обрабатываемых файлов.

Есть класс, который занимается обработкой файлов разного формата. Предполагается,
что будут добавляться новые форматы файлов. Какие вы видите проблемы в реализации?

```c#
public class FileService
{
	enum TextType
	{
    	HtmlCode,
    	RawText
	}
	public void Process(string filePath)
	{
    	var streamReader = new StreamReader(File.OpenRead(filePath));
    	var text = streamReader.ReadToEnd();
   	 
    	if (text.IndexOf("<html") != -1)
    	{
            ProcessHtmlCode(text);
    	}
    	else
    	{
            ProcessRawText(text);
    	}

    	ProcessText(
        	text,
        	text.IndexOf("<html") != -1
            	? TextType.HtmlCode
            	: TextType.RawText);
   	 
    	streamReader.Close();
	}
	private void ProcessText(string text, TextType textType)
	{
    	switch (textType)
    	{
        	case TextType.HtmlCode:
                ProcessHtmlCode(text);
                break;
        	case TextType.RawText:
                ProcessRawText(text);
                break;
        	default:
                throw new Exception("Unknown file format");
    	}
	}
	private void ProcessHtmlCode(string content){/*реализация метода*/}
	private void ProcessRawText(string content){/*реализация метода*/}
}
```

Проблема в том, что если бы мы захотели добавлять новые типы обрабатываемых файлов,
нам каждый раз пришлось бы менять внутренний метод Process. Могут быть ситуации,
когда доступа к коду данного класса может не быть, тогда непонятно, как
кастомизировать его поведение. Кроме того, даже если доступ к коду есть, если
бы этому классу пришлось обрабатывать десятки разных форматов, метод Process
разросся бы слишком сильно, что не есть хорошая практика, да и вообще затруднительно
для понимания.

Вместо этого предлагается создать интерфейс, который предстояло бы реализовать
каждому новому обработчику очередного типа файлов. Затем внутри класса FileService
завести коллекцию объектов реализующих данный интерфейс. Теперь, чтобы добавить
обработку нового типа файлов, нам нужно будет писать отдельный класс, отвечающий
за конкретно свою область, а исходный код FileService больше не будет никак
изменяться.

```c#
public interface IProcessor
{
    bool Process(string text);
    string GetType();
}

public class ConcreteProcessor : IProcessor
{
    public bool Process(string text)
    {
        /*
         * Try process the text of concrete type
         */
        
        // if (not a concrete type)
        // {
        //     return false;
        // }
        //
        return true;
    }

    public string GetType()
    {
        return "concrete type";
    }
}

public class FileService
{
    private readonly IList<IProcessor> _processors;

    public FileService(IList<IProcessor> processors)
    {
        _processors = processors;
    }

    public void AddProcessor(IProcessor processor)
    {
        _processors.Add(processor);
    }

    public void Process(string filePath)
    {
        var streamReader = new StreamReader(File.OpenRead(filePath));
        var text = streamReader.ReadToEnd();

        foreach (var processor in _processors)
        {
            if (processor.Process(text))
            {
                break;
            }
            Console.WriteLine($"Not {processor.GetType()}");
        }
 
        streamReader.Close();
    }
}
```
