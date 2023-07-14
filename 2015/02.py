file_path = "2015/inputs/02.txt"

total = 0
ribbon = 0

with open(file_path, "r") as file:
    for line in file:
        dimensions = line.split('x')
        
        l = int(dimensions[0])
        w = int(dimensions[1])
        h = int(dimensions[2])

        total += 2*l*w + 2*w*h + 2*h*l
        total += min(l*w, w*h, h*l)

        arr = [l, w, h]
        arr.sort()

        ribbon += arr[0] * 2 + arr[1] * 2
        ribbon += l*w*h

print(total)
print(ribbon)

        