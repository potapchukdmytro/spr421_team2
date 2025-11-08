module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}", 
  ],
  theme: {
    extend: {
      colors: {
        telegram: {
          DEFAULT: "#0088cc",
          light: "#2aa7e0",
        },
      },
      maxWidth: {
        'md-chat': '640px',
      }
    },
  },
  plugins: [],
}