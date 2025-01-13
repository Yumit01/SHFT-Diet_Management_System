// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAuth } from 'firebase/auth';
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyDKdppfCwgujGy6H02qPlC0soHvYSzKX5M",
  authDomain: "shft-webAPI.firebaseapp.com",
  projectId: "shft-webAPI",
  storageBucket: "shft-webAPI.firebasestorage.app",
  messagingSenderId: "900240728664",
  appId: "1:900240728664:web:822f6e499702d60032202d",
  measurementId: "G-55EZB4K22L"
};

// Initialize Firebase 

const app = initializeApp(firebaseConfig); 

export const auth = getAuth(app);