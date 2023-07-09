
document.getElementById("signupForm").addEventListener("submit", function(event) {
    event.preventDefault(); // Prevent form submission
  
    var firstName = document.getElementById("firstName").value;
    var lastName = document.getElementById("lastName").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirmPassword").value;
  
    // First Name validation
    if (firstName.length > 12) {
      alert("First Name must be up to 12 characters.");
      return;
    }
  
    // Last Name validation
    if (lastName.length > 16) {
      alert("Last Name must be up to 16 characters.");
      return;
    }
  
    // Email validation 
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      alert("Invalid Email format.");
      return;
    }
  
    // Password validation
    var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    if (!passwordRegex.test(password)) {
      alert("Password must contain at least 8 characters including 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character.");
      return;
    }
  
    // Confirm Password validation
    if (password !== confirmPassword) {
      alert("Confirm Password must match Password.");
      return;
    }
  

    var API_URL = "https://localhost:7153/api/Account/signup";

      fetch(API_URL, {
      method: "POST",
      body: JSON.stringify({
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: password
      }),   
      headers: {
        "Content-Type": "application/json"
      }
    })
      .then(async res => {
        if ( res.isError) {
          await alert("Error: " + res.messages);
        } else {
          await alert(res.messages);
        }
      })
      .catch(function(error) {
        console.error(error);
      });
  });
  