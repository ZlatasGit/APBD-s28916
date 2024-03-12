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
Console.WriteLine(CalculateAverage([4, 2, 6]));
Console.WriteLine(ReturnMax([1,2,3]));

static double CalculateAverage(int[] arr){
    int sum = 0;
    foreach(int num in arr){
        sum += num;
    }
    return sum/arr.Length;
}

static int ReturnMax(int[] ints){
    return ints.Max();
}