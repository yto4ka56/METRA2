import Foundation


    func calculateScore(data: [Int]) -> Int {
        var score = 0
        
        // 1. Условие IF (S_abs +1)
        if data.isEmpty {
            return 0
        }
        
        // 2. Цикл FOR-IN (S_abs +1)
        for value in data { // Вложенность: 1
            
            // 3. Условие IF-ELSE (S_abs +1)
            if value > 10 { // Вложенность: 2
                score += 10
            } else {
                score += value
            }
            
            // 4. Цикл WHILE (S_abs +1)
            var temp = value // Вложенность: 2
            while temp > 0 {
                temp -= 1
                
                // 5. Условие IF-ELSE IF-ELSE (S_abs +2)
                if temp % 3 == 0 { // Вложенность: 3
                    
                    // 6. Цикл REPEAT-WHILE (S_abs +1)
                    repeat { // Вложенность: 4
                        print("Iterating...")
                    } while temp < 0 // Вложенность: 4
                    
                } else if temp % 2 == 0 { // Вложенность: 3
                    // Многострочный /* комментарий */
                    /* Это не должно
                       учитываться 
                       в LOC или S_abs */
                    continue
                } else {
                    break
                }
            } // Конец WHILE
        } // Конец FOR-IN
        
        // 7. Оператор SWITCH (S_abs +1)
        switch score { // Вложенность: 1
            case 0: // S_abs +1
                print("Zero score")
            case 1...10: // S_abs +1
                print("Low score")
            default: // S_abs +1
                print("High score")
        }
        
        return score

    func anotherFunction() {
        // ...
    }
}