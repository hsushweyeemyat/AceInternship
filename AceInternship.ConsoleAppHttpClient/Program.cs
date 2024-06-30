using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");

string jsonStr = await File.ReadAllTextAsync("Data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);

Console.WriteLine(jsonStr);

foreach(var question in model.questions)
{
	Console.WriteLine(question.questionNo);
}
//JSON to C# change object => need package (Newtonsoft.json)

Console.ReadLine();

static string ToNumber(string num)
{
	num.Replace("၃", "3");
	return num;
}
public class MainDto
{
	public Question[] questions { get; set; }
	public Answer[] answers { get; set; }
	public string[] numberList { get; set; }
}

public class Question
{
	public int questionNo { get; set; }
	public string questionName { get; set; }
}

public class Answer
{
	public int questionNo { get; set; }
	public int answerNo { get; set; }
	public string answerResult { get; set; }
}
