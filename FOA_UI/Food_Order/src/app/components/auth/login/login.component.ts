import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValidateForm from 'src/app/helpers/validateform';
import { AuthService } from 'src/app/services/auth.service';
import { ToasterService } from 'src/app/services/toaster.service';
import { UserstoreService } from 'src/app/services/userstore.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;
  type: string ="password";
  isText: boolean = false;
  eyeIcon: string ='fa-eye-slash';
  // loginForm! : FormGroup;
  constructor(
    private fb:FormBuilder,
    private auth:AuthService,
    private router:Router,
    private toastService: ToasterService,
    private userStore:UserstoreService,
    ){ }
  ngOnInit(): void {
    this.loginForm =this.fb.group({
      // email:['',Validators.required],
      email: ['', [Validators.required, Validators.email]], 
      password:['',Validators.required]
    })
    
  }
  hideShow(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon="fa-eye-slash";
    this.isText ? this.type ='text':this.type ="password"
  }
  onLogin(){
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
      this.auth.login(this.loginForm.value).subscribe({
        next: (res) => {
          console.log(res.message);
          // this.loginForm.reset();
           this.auth.storeToken(res.accessToken);
           this.auth.storeRefreshToken(res.refreshToken);
           const tokenPayload = this.auth.decodedToken();
          this.userStore.setFullNameForStore(tokenPayload.name);
          this.userStore.setRoleForStore(tokenPayload.role);
       sessionStorage.setItem('key', this.loginForm.value.email);
       console.log(tokenPayload.role);
        this.loginForm.reset();
        console.log(tokenPayload.role);
        if(tokenPayload.role === 'Admin'){
          localStorage.setItem('role','Admin');
         // this.service.setAdminValue(true);
         this.auth.userStatus.next("loggedIn");
          this.router.navigateByUrl('manage');
        }else{
          localStorage.setItem('role','User');
         //  this.service.setAdminValue(false);
         this.auth.userStatus.next("loggedIn");
         this.router.navigateByUrl('list');
      
        }
          this.toastService.showSuccess("login successfull");
           //  this.router.navigate(['home'])
        },
        error: (err) => {
          this.toastService.showError("ERROR","Something when wrong!");
          console.log(err);
        },
      });
    } else {
      ValidateForm.validateAllFormFields(this.loginForm);
      this.toastService.showWarning("your form is invalid");
    }
  }
  
  get Emails(){
    return this.loginForm.get("email") as FormControl;
  }
  
  get Passs(){
    return this.loginForm.get("passs") as FormControl;
  }
    checkAdminStatus(email:string):boolean{
      return true;
      }
}
