import React, { useState } from "react";
import "./signin.css";


const SignIn = ({ onTogglePage, onLoginSuccess }) => {
   
    const [error, setError] = useState(false);
    const [errorText, setErrorText] = useState("");
    const [form, setForm] = useState({
        username: "",
        password: "",
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setForm({ ...form, [name]: value });
    };

    async function handleSubmit(event) {
        event.preventDefault();
        try {
            const response = await fetch('https://localhost:7231/api/User/authenticate', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(form)
            });

            if (!response.ok) {
                throw new Error('Invalid username or password');
            }

            const userData = await response.json();
            onLoginSuccess(userData); 
            console.log(userData);
        } catch (error) {
            setError(true);
            setErrorText(error.message);
        }
    }

    return (
        <div className="signinall">
            <div className="centered-div">
                <div className="sign-in-section">
                    <h2 style={{ textAlign: "center", marginTop: 30, marginBottom: "30px" }}>Sign in</h2>

                    <form onSubmit={handleSubmit}>
                        <input
                            style={{ display: "block", margin: "10px auto", marginBottom: "20px" }}
                            type="text"
                            id="username"
                            name="username"
                            placeholder="Username"
                            onChange={handleInputChange}
                            required
                        />
                        <input
                            style={{ display: "block", margin: "10px auto", marginBottom: "30px" }}
                            type="password"
                            id="password"
                            name="password"
                            placeholder="Password"
                            onChange={handleInputChange}
                            required
                        />

                        <p
                            style={{ display: "block", margin: "10px auto", marginBottom: "30px", color: "red", textAlign: "center" }}
                        >
                            {error ? errorText : " "}
                        </p>
                        <input
                            style={{ display: "block", margin: "10px auto", marginBottom: "30px" }}
                            type="submit"
                            value={"Sign In"}
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
                        Don't have an account ? Create one now !
                    </button>
                </div>
            </div>
        </div>
    );
};

export default SignIn;
