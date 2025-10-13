import Foundation


   /* func calculateScore(data: [Int]) -> Int {
        var score = 0
        
        if data.isEmpty {
            return 0
        }
        
        for value in data { 
            
  
            if value > 10 {
                score += 10
            } else {
                score += value
            }
            
            var temp = value
            while temp > 0 {
                temp -= 1
                
                
                if temp % 3 == 0 { 
                    
                    
                    repeat {
                        print("Iterating...")
                    } while temp < 0
                    
                } else if temp % 2 == 0 { 
                    
                    continue
                } else {
                    break
                }
            } 
        } 
        
      
        switch score { 
            case 0: 
                print("Zero score")
            case 1...10: 
                print("Low score")
            default: 
                print("High score")
        }
        
        return score

    func anotherFunction() {
        // ...
    }
}*/

    if value > 10 {
                score += 10
            } else {
                score += value
            
            
            var temp = value
            while temp > 0 {
                temp -= 1
                
                
                if temp % 3 == 0 { 
                    
                    
                    switch score { 
                                case 0: 
                                    print("Zero score")
                                case 1...10: 
                                    print("Low score")
                                default: 
                                    print("High score")
                            }
                    
                } 
            } 
            }