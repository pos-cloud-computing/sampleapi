pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore packages') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Unit Tests') {
            steps {
                sh 'dotnet test --logger "trx;LogFileName=testresults.trx"'
            }
        }

        stage('Publish Test Results') {
            steps {
                junit 'testresults.trx'
            }
        }
    }
}
