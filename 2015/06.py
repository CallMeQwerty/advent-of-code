# State: 1 - ON, 2 - OFF, 3 - Toggle
def update_lights(lights_grid, state, start, finish):
    start_x, start_y = map(int, start.split(","))
    finish_x, finish_y = map(int, finish.split(","))

    if state == 1:
        for i in range(start_x, finish_x + 1):
            for j in range(start_y, finish_y + 1):
                lights_grid[i][j] += 1
    elif state == 2:
        for i in range(start_x, finish_x + 1):
            for j in range(start_y, finish_y + 1):
                lights_grid[i][j] = max(0, lights_grid[i][j] - 1)
    elif state == 3:
        for i in range(start_x, finish_x + 1):
            for j in range(start_y, finish_y + 1):
                lights_grid[i][j] += 2

    return lights_grid


file_path = "2015/inputs/06.txt"

with open(file_path, "r") as file:
    file_contents = file.readlines()

lights_array = [[0 for _ in range(1000)] for _ in range(1000)]

for lane in file_contents:
    words = lane.split()
    operation = words[0]
    
    if (operation == "turn"):
        state = 1 if words[1] == "on" else 2
        lights_array = update_lights(lights_array, state, words[2], words[4])
    elif (operation == "toggle"):
        lights_array = update_lights(lights_array, 3, words[1], words[3])

light_count = 0

for i in range(1000):
    for j in range(1000):
        light_count += lights_array[i][j]            

print(light_count)
        