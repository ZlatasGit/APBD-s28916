Console.WriteLine("Enter an integer: ");
string userInput = Console.ReadLine();
int number = int.Parse(userInput);
if(number<0){
    Console.WriteLine(number+" is smaller than zero");
} else if(number>0){
    Console.WriteLine(number+" is greater than zero");
} else {
    Console.WriteLine("entered number is zero");
}