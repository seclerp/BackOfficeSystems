<template>
    <div class="md-layout md-gutter md-alignment-top-center">
        <div class="md-layout-item md-size-50 md-medium-size-33 md-small-size-50 md-xsmall-size-100">
            <SumOfInventoryTable :data="sumOfInventoryData" />
        </div>
    </div>
</template>

<script>
    import SumOfInventoryTable from "../components/SumOfInventoryTable";

    export default {
        name: "SumOfInventory",
        data: () => ({
            sumOfInventoryData: []
        }),
        components: {
            SumOfInventoryTable
        },
        async mounted() {
            let response = await fetch(
                `${process.env.VUE_APP_API_ROOT}/brand/sum-of-inventory`,
                {
                    headers: {
                        'Accept': 'application/json'
                    }
                }
            );
            if (response.ok) {
                this.sumOfInventoryData = await response.json();
            }
        }
    }
</script>

<style scoped>
    .md-layout {
        width: 100%;
    }
</style>