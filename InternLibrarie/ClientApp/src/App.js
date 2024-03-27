import React, { useState } from 'react';
import SignIn from './components/Signin';
import SignUp from './components/Signup';
import Home from './components/Home';
import AdminDashboard from './components/AdminDashboard';
import Profile from './components/Profile'; // Import the Profile component

function App() {
    const [isLoginPage, setIsLoginPage] = useState(false);
    const [userId, setUserId] = useState(null);
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [isAdmin, setIsAdmin] = useState(false);
    const [showProfile, setShowProfile] = useState(false); // State to control profile page visibility

    const handleTogglePage = () => {
        setIsLoginPage(!isLoginPage);
    };

    const handleLoginSuccess = (userData) => {
        setIsLoggedIn(true);
        setUserId(userData.id);
        setIsAdmin(userData.username === 'admin');
    };

    const handleSignUpSuccess = (userData) => {
        setIsLoggedIn(true);
        setUserId(userData);
    };

    const handleSignOut = () => {
        setIsLoggedIn(false);
        setUserId(null);
        setIsAdmin(false);
        setIsLoginPage(true);
        setShowProfile(false); // Hide profile page on sign out
    };

    const navigateToProfile = () => {
        setShowProfile(true);
    };

    return (
        <div>
            {isLoggedIn ? (
                <>
                    {showProfile ? (
                        <Profile userId={userId} onSignOut={handleSignOut} />
                    ) : (
                        isAdmin ? <AdminDashboard /> : (userId ? <Home userId={userId} onSignOut={handleSignOut} navigateToProfile={navigateToProfile} /> : null)
                    )}
                </>
            ) : (
                <>
                    {isLoginPage ? (
                        <SignIn onLoginSuccess={handleLoginSuccess} onTogglePage={handleTogglePage} />
                    ) : (
                        <SignUp onSignUpSuccess={handleSignUpSuccess} onTogglePage={handleTogglePage} />
                    )}
                </>
            )}
        </div>
    );
}

export default App;
