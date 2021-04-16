function login(){
    //n1. lay thong tin form dang nhap
    let email = document.getElementById("lgEmail").value;
    let pass = document.getElementById("lgPassword").value;
    //buoc 2 goi api dang nhap
   
    let userLogin ={
        "email": email,
        "password": pass
    };

    axios({
        url:'http://localhost:8080/api/admin/auth/login',
        method: 'POST',
        data: 'userLogin'
    })
    .then(function(resp){
        document.getElementById("lgEmail").value ="";
        document.getElementById("lgPassword").value ="";

        localStorage.setItem('USER_TOKEN', resp.data)

        document.location.href = "role/role-index.html"
    })
    .catch(function(err){
        console.log(err.response)
    })
    //luu token vao may nguoi dung
}