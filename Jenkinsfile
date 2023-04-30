pipeline {
    agent any


    stages {

        stage ('Clean workspace') {
            steps {
                cleanWs()
            }
        }
        stage ('Checkout') {
            steps {
                script {
                    checkout scm
                }
            }
        }
      /*  stage ('Sonarqube validation') {
            steps {
                script {
                    scannerHome = tool 'sonar'
                }
                withSonarQubeEnv('sq1'){
                    sh "${scannerHome}/bin/sonar-scanner -Dsonar.projectKey=sampleapi -Dsonar.sources=. -Dsonar.host.url=http://18.207.126.25:9000 -Dsonar.login=squ_70312d9e6984b0b573fcfdd1ce1ba5699a676db2"
                }
            }
        }*/
        stage('Test: Unit Test'){
        steps {
            sh 'dotnet test --logger "trx;LogFileName=UnitTests.trx"'
            }
        }
        stage('Publish Image ECR'){
            steps {
                withEnv(["AWS_ACCESS_KEY_ID=${env.AWS_ACCESS_KEY_ID}", "AWS_SECRET_ACCESS_KEY=${env.AWS_SECRET_ACCESS_KEY}", "AWS_DEFAULT_REGION=${env.AWS_DEFAULT_REGION}"]){
                 sh 'docker login -u AWS -p $(aws ecr-public get-login-password --region us-east-1) public.ecr.aws/p3q4d0z4'
                 sh 'docker build -t public.ecr.aws/p3q4d0z4/sampleapi:""$BUILD_ID"" -f ./Web.API/Dockerfile .' 
                 sh 'docker push public.ecr.aws/p3q4d0z4/sampleapi:""$BUILD_ID""'

                 slackSend (color: 'good', message: "[ Sucesso ] O novo build versão:" + BUILD_ID + " Gerado com sucesso. ", tokenCredentialId: 'slack-secret')
                }
            }
        }

        stage ('Deploy HML') {
        
            steps {
                withKubeConfig ([credentialsId: 'eks-hml']) {
                    sh 'sed -i "s/{{TAG}}/$BUILD_ID/g" ./k8s/deployment.yaml'
                    sh 'kubectl apply -f ./k8s/deployment.yaml'
                }                
            }
        }
        stage('Notifying user') {
            steps {
                slackSend (color: 'good', message: "[ Sucesso ] O novo build versão:" + BUILD_ID + " esta disponivel no amabiente de homologação. ", tokenCredentialId: 'slack-secret')
            }
        }

       stage('Do you want to deploy to production?') {
            steps {
                script {
                    slackSend (color: 'warning', message: "Para aplicar a mudança em produção, acesse [Janela de 30 minutos]: ${JOB_URL}", tokenCredentialId: 'slack-secret')
                    timeout(time: 30, unit: 'MINUTES') {
                        input(id: "deploy-gate", message: "Deploy em produção?", ok: 'Deploy')
                    }
                }
            }
        }
        stage ('Deploy PRD') {
            steps {
                script {
                    try {
                        build job: 'cd-simple-prd', parameters: [[$class: 'StringParameterValue', name: 'VERSION_IMAGE', value: BUILD_ID]]
                    } catch (Exception e) {
                        slackSend (color: 'error', message: "[ FALHA ] Não foi possivel subir o container em producao", tokenCredentialId: 'slack-secret')
                        sh "echo $e"
                        currentBuild.result = 'ABORTED'
                        error('Erro')
                    }
                }
            }
        }
   }
}

