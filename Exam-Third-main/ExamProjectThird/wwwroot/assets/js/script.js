let myNav=document.getElementById("Navbar");
let aboutSec=document.getElementById("About")
let tag=document.getElementById("Port");

window.onscroll=function(){
    if (document.body.scrollTop >= 100 || document.documentElement.scrollTop >= 100 ){
        
       
        myNav.classList.add("nav-color");
        //myNav.style.background= rgba(26, 24, 22, 0.85);
      
       
      }  else {
         
          myNav.classList.remove("nav-color");
      }
     
     // console.log(myNav);
}




