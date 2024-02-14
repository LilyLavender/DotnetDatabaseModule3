// Ask user for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// Input response
string? resp = Console.ReadLine();

if (resp == "1") { // Create data file
    // Ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // Input the response (convert to int)
    int weeks = int.Parse(Console.ReadLine());
    // Determine start and end date
    DateTime today = DateTime.Now;
    // We want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // Subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // Random number generator
    Random rnd = new Random();
    // Create file
    StreamWriter sw = new StreamWriter("data.txt");

    // Loop for the desired # of weeks
    while (dataDate < dataEndDate) {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // Generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // Add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
} else if (resp == "2") {
    // Parse data file

    // Check if file exists
    if (!File.Exists("data.txt")) {
        Console.WriteLine("data.txt does not exist! You can create one with option 1.");
    } else {
        StreamReader sr = new StreamReader("data.txt");

        string? line = "";
        while ((line = sr.ReadLine()) != null) {
            
            // Get a date object from the line.
            string stringDate = line.Substring(0, line.IndexOf(","));
            string[] datePartsString = stringDate.Split("/");
            int[] dateParts = Array.ConvertAll(datePartsString,Int32.Parse);
            DateTime week = new DateTime(dateParts[2], dateParts[0], dateParts[1]); // I love American date formats 🥳
            // Print week header
            Console.WriteLine($"Week of {week:MMM}, {week:dd}, {week:yyyy}");

            // Print lines 2 & 3
            Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg");
            Console.WriteLine(" -- -- -- -- -- -- -- --- ---");

            // Print hours
            string[] hoursString = line.Substring(line.IndexOf(",") + 1).Split("|");
            int[] hours = Array.ConvertAll(hoursString,Int32.Parse);
            foreach (int hour in hours) {
                Console.Write($"{hour,3}");
            }
            Console.Write($"{hours.Sum(),4} {hours.Average():F1}\n\n"); // ec

        }

        sr.Close();
    }
}
