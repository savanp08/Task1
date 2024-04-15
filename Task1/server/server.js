const express = require('express');
const app = express();
const path = require('path');

// Serve static files from a specified directory
const videosPath = path.join(__dirname, 'videos');
app.use(express.static(videosPath));

// Start the server
const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
});
