pipeline {
    agent any

    tools {
        dotnetsdk 'dotnet8'
    }

    stages {

        stage('Git Checkout') {
            steps {
                git branch: 'main',
                    url: 'https://github.com/suryaprakash-bandoju/3-Tier-DotNet-MangoDB-Community-Board-Project.git'
            }
        }

        stage('Restoring Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Building') {
            steps {
                sh 'dotnet build'
            }
        }

    }
}
