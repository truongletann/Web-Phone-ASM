function checkLogin(){
    let token = localStorage.getItem('USER_TOKEN');
    if(token == null || token.length ==0){
        document.location.href = "/login.html";
    }

}
function logout(){
    let token = localStorage.getItem('USER_TOKEN');
    if(token == null || token.length ==0){
        localStorage.removeItem('USER_TOKEN');
        document.location.href = "/login.html";
    }
}

checkLogin;
