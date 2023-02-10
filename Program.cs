using CA230210;
Random rnd = new();
GameOfLife glf = new(30, 30, rnd.Next());

while (true) glf.Simulate();
