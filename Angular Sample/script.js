var app = angular.module('angularjs-starter', []);

//controller
app.controller('main', function($scope){
 
  $scope.skillsArray1 = ['.NET', 'Android', 'Objective C', 'C++', 'C#', 'Linux', 'Windows PowerShell'];
  $scope.skillsArray2 = ['One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight','Nine', 'Ten'];
  $scope.skillsArray3 = ['Java', 'jQuery', 'Angular', 'CSS', 'HTML', 'Ruby', 'Python', 'React.js', 'JSON'];
  
  
});


//directives
app.directive('clickOff', function($parse, $document) {
  var dir = {
        compile: function( $element, attr) {
          // Parse the expression to be executed
          // whenever someone clicks _off_ this element.
          var fn = $parse(attr["clickOff"]);
          return function(scope, element, attr) {
            // add a click handler to the element that
            // stops the event propagation.
            element.bind("click", function(event) {
                
                event.stopPropagation();
                
            });
            
            angular.element($document[0].body).bind("click",
            function(event) {
                scope.$apply(function() {
                    fn(scope, {$event:event});
                    //console.log("showAllSkills1 is:"+ scope.showAllSkills1);
                    //console.log("showAllSkills2 is:"+ scope.showAllSkills2);
                    //console.log("showAllSkills3 is:"+ scope.showAllSkills3);
                });
            });
          };
        }
      };
    return dir;
});



app.directive('clickOffin', function($document) {
  return{
    restrict: 'A',
    link: function(scope, element, attr){
      
      scope.toggle = function(e){
         $(".searching").addClass("hide");
         $(element).parent().next().toggleClass("hide");
        //$(element).parent().next().removeClass("hide");
         e.stopPropagation();
       
        
      }
  
     $document.on('click', function(event){
      // event.stopPropagation();
       $(".searching").addClass("hide");
       
       
    });
      
      
    }
  };
});

/*Other Test Directives******************************************/ 

app.directive('clickAway', function($parse, $document) {
  var dir = {
        compile: function( $element, attr) {
          // Parse the expression to be executed
          // whenever someone clicks _off_ this element.
          var fn = $parse(attr["clickOff"]);
          return function(scope, element, attr) {
            // add a click handler to the element that
            // stops the event propagation.
            element.bind("click", function(event) {
                $(this)
                event.stopPropagation();
                //console.log("showAllSkills1 scope is:"+  element.html() );
                console.log("showAllSkills1 is:"+ scope.showAllSkills1);
                console.log("showAllSkills2 is:"+ scope.showAllSkills2);
                console.log("showAllSkills3 is:"+ scope.showAllSkills3);
            });
            
            angular.element($document[0].body).bind("click",
            function(event) {
                scope.$apply(function() {
                    fn(scope, {$event:event});
                    console.log("showAllSkills1 is:"+ scope.showAllSkills1);
                    console.log("showAllSkills2 is:"+ scope.showAllSkills2);
                    console.log("showAllSkills3 is:"+ scope.showAllSkills3);
                });
            });
          };
        }
      };
    return dir;
});

   
    
app.directive('cOff', function($parse, $document) {
  var dir = {
        compile: function($element, attr) {
          
          var fn = $parse(attr["cOff"]);
          
          return function(scope, element, attr) {
            
           /*
            var hideElement= function(){
              scope.toggle = function(){
                    scope.showAllSkills = !scope.showAllSkills;
              };
              event.stopPropagation();
              var isClickedElementChildOfPopup = false ;
          
            if (isClickedElementChildOfPopup)
              return;
              
            if (!isClickedElementChildOfPopup)
              scope.showAllSkills = true;
              return scope.showAllSkills;
              //scope.showAllSkills = false;
            };
             */
            scope.toggle = function(){
              console.log("showAllSkills2 is:"+ scope.showAllSkills2);
              //scope.showAllSkills2 = !scope.showAllSkills2;
              if(scope.showAllSkills2){ 
                scope.showAllSkills2 =false;
                
              } else if(!scope.showAllSkills2){
                scope.showAllSkills2 =true;}
              // scope.showAllSkills = false;
             // event.stopPropagation();
              console.log("showAllSkills2 is:"+ scope.showAllSkills2);
           };
            angular.element($document[0].body).bind("click",
            function(event) {
                
                scope.$apply(function() {
                    console.log("showAllSkills1 is:"+ scope.showAllSkills1);
                    console.log("showAllSkills2 is:"+ scope.showAllSkills2);
                    console.log("showAllSkills3 is:"+ scope.showAllSkills3);
                    fn(scope, {$event:event});
                    console.log("showAllSkills1 is:"+ scope.showAllSkills1);
                    console.log("showAllSkills2 is:"+ scope.showAllSkills2);
                    console.log("showAllSkills3 is:"+ scope.showAllSkills3);
                    //scope.showAllSkills = false;
                });
            });
          };
        }
      };
    return dir;
});