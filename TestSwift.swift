import Foundation

let numbers = [15, 3, 8, 0, 42, 7, 25, 18, 2, 9]
var result = ""

for number in numbers {
    if number > 0 {
      
        if number % 2 == 0 {
            result += "четное\n"
        } else {
            result += "нечетное\n"
        }
    }
}

var counter = 0
while counter < 6 {
    if counter < numbers.count {
 
        if numbers[counter] == 0 {
            print("Найден ноль на позиции \(counter)")
            
    
            switch counter {
            case 0:
                print("Ноль в начале массива")
            case numbers.count-1:
                print("Ноль в конце массива")
            default:
                print("Ноль в середине массива")
            }
        }
    }
    counter += 1
}

print(result)