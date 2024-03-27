import React, { useState } from "react";
import defaultImage from "./th.jpeg";
import "./signup.css";


const SignUp = ({ onTogglePage, onSignUpSuccess }) => {
    
    const [error, setError] = useState(false);
    const [conf, setConf] = useState("");
    const [profileImage, setProfileImage] = useState(defaultImage);
    const [errorText, setErrorText] = useState("");
    const [form, setForm] = useState({
        userName: "",
        email: "",
        password: "",
        subscriptionType: "normal",
        
    });

    const handleConfPass = (e) => {
        setConf(e.target.value);
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setForm({ ...form, [name]: value });
    };

    function isValidEmail(email) {

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        return emailRegex.test(email);
    }
    const handleSubmit = async (event) => {
        event.preventDefault();
        if (form.password !== conf) {
            setError(true);
            setErrorText("Passwords Mismatch");
        } else if (!isValidEmail(form.email)) {
            setError(true);
            setErrorText("Invalid email");
        } else if (form.userName.length < 5 || form.userName.length > 10) {
            setError(true);
            setErrorText("Username is too short/long");
        } else if (form.password.length < 5 || form.password.length > 10) {
            setError(true);
            setErrorText("Password is too short/long");
        } else {
            setError(false);
            try {
                const response = await fetch('https://localhost:7231/api/User/register', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(form) 
                });

                if (response.ok) {
                    const responseData = await response.json();
                    const userData = responseData ? JSON.parse(responseData) : null;
                    console.log("Response from server:", userData); 
                    if (userData ) {
                        onSignUpSuccess(userData);

                    } else {
                        setError(true);
                        setErrorText("User data is missing or invalid");
                        console.log("Unexpected response from server:", userData);
                    }
                } else if (response.status === 400) {
                    setError(true);
                    setErrorText('User already exists');
                } else {
                    setError(true);
                    setErrorText("Failed to register: " + response.statusText);
                    console.error("Failed to register:", response);
                }

            } catch (error) {
                setError(true);
                setErrorText("Registration failed: " + error.message);
                console.error("Registration failed:", error);
            }
        }
    };






    return (
        <div className="signup-container">
            <div className="centered-div">
                <div className="sign-in-section">
                    <h2 style={{ textAlign: "center", marginTop: 30, marginBottom: "30px" }}>Sign Up</h2>

                    <div>
                        <div>
                            <img
                                src={profileImage}
                                alt="File Preview"
                                style={{ display: "block", width: "150px", height: "150px", margin: "10px auto", borderRadius: "50%" }}
                            />
                        </div>
                        <form onSubmit={handleSubmit}>

                            <input
                                style={{ display: "block", margin: "10px auto", marginBottom: "20px" }}
                                className="custom-file-input"
                                type="text"
                                id="username"
                                onChange={handleInputChange}
                                name="userName"
                                placeholder="Username"
                                required
                            />

                            <input
                                style={{ display: "block", margin: "10px auto", marginBottom: "20px" }}
                                type="text"
                                id="email"
                                name="email"
                                onChange={handleInputChange}
                                placeholder="Email"
                                required
                            />
                            <input
                                style={{ display: "block", margin: "10px auto", marginBottom: "30px" }}
                                type="password"
                                id="password"
                                name="password"
                                onChange={handleInputChange}
                                placeholder="Password"
                                required
                            />

                            <input
                                style={{ display: "block", margin: "10px auto", marginBottom: "20px" }}
                                type="password"
                                id="confpassword"
                                name="confpassword"
                                onChange={handleConfPass}
                                placeholder="Confirm Password"
                                required
                            />

                            <select
                                style={{ display: "block", margin: "10px auto", marginBottom: "20px" }}
                                value={form.subscriptionType}
                                onChange={handleInputChange}
                                name="subscriptionType"
                                required
                            >
                                <option value="normal">Normal</option>
                                <option value="premium">Premium</option>
                            </select>

                            <p
                                style={{ display: "block", margin: "10px auto", marginBottom: "30px", color: "red", textAlign: "center" }}
                            >
                                {error ? errorText : " "}
                            </p>

                            <input
                                style={{ display: "block", margin: "10px auto", marginBottom: "30px" }}
                                type="submit"
                                value={"Sign Up"}
                            />
                        </form>

                        <button
                            style={{
                                display: "block",
                                margin: "10px auto",
                                marginBottom: "20px",
                                color: "rgba(28, 71, 112, 0.7)",
                                textAlign: "center",
                                backgroundColor: "white",
                                boxShadow: "0px 0px 0px 0px",
                            }}
                            onClick={onTogglePage}
                        >
                            Already Have an account ? Log in now !
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SignUp;
