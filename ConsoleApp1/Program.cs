Console.WriteLine("Enter an integer: ");
string userInput = Console.ReadLine();
int number = int.Parse(userInput);
if(number<0){
    Console.WriteLine(number+" is smaller than zero!");
} else if(number>0){
    Console.WriteLine(number+" is greater than zero!");
} else {
    Console.WriteLine("entered number is zero");
}
Console.WriteLine(myFunc([4, 2, 6]));
Console.WriteLine(returnMax([1,2,3]));

static double myFunc(int[] ints){
    int sum = 0;
    foreach(int num in ints){
        sum += num;
    }
    return sum/ints.Length;
}

static int returnMax(int[] ints){
    return ints.Max();
}