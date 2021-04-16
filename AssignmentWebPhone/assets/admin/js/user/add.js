function loadRole(){
    axios({
        url:"http://localhost:8080/api/admin/role",
        method: 'GET'
    })
    .then(function(resp){
        let arrRole = resp.data;
        let strOption = "";
        // for (let i = 0; i < arrRole.length; i++) {
        //     let role = arrRole[i];           
        // }
        for (let role of arrRole) {
            strOption += `<option value="${role.id}">${role.name}</option>`;
           // console.log(strOption)
        }

        let roleIDTag = document.getElementById('roleID');

        roleIDTag.innerHTML = strOption;
    })
    .catch(function(err){
        console.log(err.response);
    })

    
}

loadRole();

function addRole() {
    let check = true;
    let fullname = document.getElementById('fullname').value;
    if(fullname.length == 0){
        document.getElementById('fullnameErr').innerHTML = 'Vui long nhap ho ten!';
        check = false;
    }else{
        document.getElementById('fullnameErr').innerHTML = '';
        check = true;
    }

    let email = document.getElementById('email').value;
    if(email.length == 0){
        document.getElementById('emailErr').innerHTML = 'Vui long nhap email!';
        check = false;
    }
    else if( (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(email) === false)){
        document.getElementById('emailErr').innerHTML = 'Email khong dung dinh dang';
        check = false;
    }
    else{
        document.getElementById('emailErr').innerHTML = '';
        check = true;
    }

    let password = document.getElementById('password').value;
    if(password.length == 0){
        document.getElementById('passwordErr').innerHTML = 'Vui long nhap password!';
        check = false;
    }else{
        document.getElementById('passwordErr').innerHTML = '';
        check = true;
    }

    let confirm = document.getElementById('confirm').value;
    if(confirm.length == 0){
        document.getElementById('confirmErr').innerHTML = 'Vui long nhap lai mat khau!';
        check = false;
    }
    else if(confirm != password){
        document.getElementById('confirmErr').innerHTML = 'khong khop mat khau';
        check = false;
    }
    else{
        document.getElementById('confirmErr').innerHTML = '';
        check = true;
    }
    let roleId = document.getElementById('roleID').value;
    if(check){
        let user={
            "fullname": fullname,
            "email": email,
            "password": password,
            "confirm": confirm,
            "avatar": "",
            "roleId": roleId
        }
    
        axios({
            url:"http://localhost:8080/api/admin/user",
            method: 'POST',
            data: user
        })
        .then(function(resp){
            console.log(user)
            console.log('them thanh cong')
        })
        .catch(function(err){
            console.log(err.response);
        })
    }
    //console.log(roleId)
    
}