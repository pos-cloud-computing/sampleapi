pipeline {
    agent any

    stages {
        stage ('Build Docker image') {
            steps {
                script {
                    dockerapp = docker.build("995396735443.dkr.ecr.us-east-1.amazonaws.com/sampleapi:${env.BUILD_ID}", '-f ./Web.API/Dockerfile . ')
                }
            }
        }
        stage('SonarQube Analysis') {
            def scannerHome = tool 'SonarScanner for MSBuild'
            withSonarQubeEnv() {
            sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:\"pos-cloud-computing\""
            sh "dotnet build"
            sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll end"
            }
        }    
   }
}

